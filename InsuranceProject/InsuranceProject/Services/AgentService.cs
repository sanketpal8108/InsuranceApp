using InsuranceDay1.Models;
using InsuranceProject.Data;
using InsuranceProject.Repository;

namespace InsuranceProject.Services
{
    public class AgentService:IAgentService
    {
        private IEntityRepository<Agent> _entityRepository;
        private MyContext _context;

        public AgentService(IEntityRepository<Agent> entityRepository,MyContext context)
        {
            _entityRepository = entityRepository;
            _context = context;
        }

        public List<Agent> GetAll()
        {
            var agentQuery = _entityRepository.Get();
            var agents = agentQuery.Where(agent => agent.IsActive).ToList();
            return agents;
        }

        public Agent Get(int id)
        {
            var agentQuery = _entityRepository.Get();
            var agent = agentQuery.Where(agent => agent.Id == id).FirstOrDefault();
            return agent;
        }

        public Agent Check(int id)
        {
            return _entityRepository.Get(id);
        }

        public int Add(Agent agent)
        {
            return _entityRepository.Add(agent);
        }

        public Agent Update(Agent agent)
        {
            return _entityRepository.Update(agent);
        }

        public void Delete(Agent agent)
        {
            _entityRepository.Delete(agent);
        }

        public Agent FindAgent(string username)
        {
            return _context.Agents.Where(user => user.UserName == username).FirstOrDefault();
        }

        public string GetRoleName(Agent agent)
        {
            return _context.Roles.Where(role => role.Id == agent.RoleId).FirstOrDefault().RoleName;
        }

    }
}
