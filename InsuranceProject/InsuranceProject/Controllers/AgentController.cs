using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private IAgentService _agentService;
        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }
        [HttpGet]
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
            return BadRequest("Contacts not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var agent = _agentService.Get(id);
            if (agent == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(agent));
        }
        [HttpPost]
        public IActionResult Add(AgentDto agentDto)
        {
            var agent = ConvertToModel(agentDto);
            var agentId = _agentService.Add(agent);
            if (agentId == null)
                return BadRequest("Some errors Occurred");
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
            return BadRequest("No contact found to update");
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
            return BadRequest("No contact found to delete");
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
                IsActive = true

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
                Email = agent.Email,
                UserName = agent.UserName,
                Commision = agent.Commision,
                TotalCommision = agent.TotalCommision,
                CountCustomer = agent.Customers != null ? agent.Customers.Count : 0,
                CountCommision=agent.Commisions != null ? agent.Commisions.Count : 0,

            };
        }
    }
}
