using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Exceptions;
using InsuranceProject.Services;
using InsuranceProject.Token_Creation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private IAgentService _agentService;
        private IConfiguration _configuration;
        public AgentController(IAgentService agentService, IConfiguration configuration)
        {
            _agentService = agentService;
            _configuration = configuration;

        }
        [HttpGet,Authorize(Roles ="Admin")]
        public IActionResult Get()
        {
            var agentDTO = new List<AgentDto>();
            var agents = _agentService.GetAll();
            if (agents.Count > 0)
            {
                foreach (var agent in agents)
                {
                    agentDTO.Add(ConvertToDTO(agent));
                }
                return Ok(agentDTO);
            }
            throw new EntityNotFoundError("Agent not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var agent = _agentService.Get(id);
            if (agent == null)
            {
                throw new EntityNotFoundError("Agent not found");
            }
            return Ok(ConvertToDTO(agent));
        }
        [HttpPost]
        public IActionResult Add(AgentDto agentDto)
        {
            var agent = ConvertToModel(agentDto);
            agent.Password = BCrypt.Net.BCrypt.HashPassword(agentDto.Password);
            var agentId = _agentService.Add(agent);
            if (agentId == null)
                throw new EntityInsertError("Some errors Occurred");
            return Ok(agentId);
        }
        [HttpPut]
        public IActionResult Update(AgentDto agentDto)
        {
            var agentDTOToUpdate = _agentService.Check(agentDto.Id);
            if (agentDTOToUpdate != null)
            {
                var updatedAgent = ConvertToModel(agentDto);
                var modifiedAgent = _agentService.Update(updatedAgent);
                return Ok(ConvertToDTO(modifiedAgent));
            }
            throw new EntityNotFoundError("No Agent found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var agent = _agentService.Get(id);
            if (agent != null)
            {
                _agentService.Delete(agent);
                return Ok(id);
            }
            throw new EntityNotFoundError("No Agent found to delete");
        }

        [HttpPost("Login")]

        public IActionResult Login(AgentDto agentDto)
        {
            var agent = _agentService.FindAgent(agentDto.UserName);
            //admin.RoleId = 1;
            var role = _agentService.GetRoleName(agent);
            if (agent != null)
            {
                if (BCrypt.Net.BCrypt.Verify(agentDto.Password, agent.Password))
                {
                    //return Ok("Login Successful");
                    string jwt = CreateToken<Agent>.CreateTokens(agent.UserName, role, _configuration);
                    return Ok(jwt);
                }
            }
            return BadRequest("UserName/Password dosesnt exist");
        }
        private Agent ConvertToModel(AgentDto agentDto)
        {
            return new Agent()
            {
                Id = agentDto.Id,
                FirstName = agentDto.FirstName,
                LastName = agentDto.LastName,
                MobileNumber = agentDto.MobileNumber,
                Email = agentDto.Email,
                UserName = agentDto.UserName,
                Commision = agentDto.Commision,
                TotalCommision = agentDto.TotalCommision,
                Password = agentDto.Password,
                IsActive = true,
                RoleId = 2,

            };
        }
        private AgentDto ConvertToDTO(Agent agent)
        {
            return new AgentDto()
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                MobileNumber = agent.MobileNumber,
                Password = agent.Password,
                Email = agent.Email,
                UserName = agent.UserName,
                Commision = agent.Commision,
                TotalCommision = agent.TotalCommision,
                CountCustomer = agent.Customers != null ? agent.Customers.Count : 0,
                CountCommision=agent.Commisions != null ? agent.Commisions.Count : 0,
                //RoleId=agent.RoleId,
            };
        }
    }
}
