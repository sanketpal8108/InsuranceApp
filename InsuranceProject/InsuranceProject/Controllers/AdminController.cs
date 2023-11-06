using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var adminDTO = new List<AdminDto>();
            var admins = _adminService.GetAll();
            if (admins.Count > 0)
            {
                foreach (var admin in admins)
                {
                    adminDTO.Add(ConvertToDTO(admin));
                }
                return Ok(adminDTO);
            }
            return BadRequest("Contacts not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(admin));
        }
        [HttpPost]
        public IActionResult Add(AdminDto adminDto)
        {
            var admin = ConvertToModel(adminDto);
            var AdminId = _adminService.Add(admin);
            if (AdminId == null)
                return BadRequest("Some errors Occurred");
            return Ok(AdminId);
        }
        [HttpPut]
        public IActionResult Update(AdminDto adminDto)
        {
            var adminDTOToUpdate = _adminService.Check(adminDto.Id);
            if (adminDTOToUpdate != null)
            {
                var updatedAdmin = ConvertToModel(adminDto);
                var modifiedAdmin = _adminService.Update(updatedAdmin);
                return Ok(ConvertToDTO(modifiedAdmin));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var agent = _adminService.Get(id);
            if (agent != null)
            {
                _adminService.Delete(agent);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private Admin ConvertToModel(AdminDto agentDto)
        {
            return new Admin()
            {
                Id = agentDto.Id,
                FirstName = agentDto.FirstName,
                LastName = agentDto.LastName,
                UserName = agentDto.UserName,
                IsActive = true

            };
        }
        private AdminDto ConvertToDTO(Admin admin)
        {
            return new AdminDto()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                UserName = admin.UserName,
               

            };
        }
    }
}
