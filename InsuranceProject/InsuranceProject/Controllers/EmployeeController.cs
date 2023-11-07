using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var employeeDTO = new List<EmployeeDto>();
            var employees = _employeeService.GetAll();
            if (employees.Count > 0)
            {
                foreach (var employee in employees)
                {
                    employeeDTO.Add(ConvertToDTO(employee));
                }
                return Ok(employeeDTO);
            }
            return BadRequest("Contacts not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(employee));
        }
        [HttpPost]
        public IActionResult Add(EmployeeDto employeeDto)
        {
            var employee = ConvertToModel(employeeDto);
            var employeeId = _employeeService.Add(employee);
            if (employeeId == null)
                return BadRequest("Some errors Occurred");
            return Ok(employeeId);
        }
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var employeeDTOToUpdate = _employeeService.Check(employeeDto.Id);
            if (employeeDTOToUpdate != null)
            {
                var updatedEmployee = ConvertToModel(employeeDto);
                var modifiedEmployee = _employeeService.Update(updatedEmployee);
                return Ok(ConvertToDTO(modifiedEmployee));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee != null)
            {
                _employeeService.Delete(employee);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private Employee ConvertToModel(EmployeeDto employeeDto)
        {
            return new Employee()
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                UserName = employeeDto.UserName,
                Password = employeeDto.Password,
                IsActive = true

            };
        }
        private EmployeeDto ConvertToDTO(Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                UserName = employee.UserName,
                Password = employee.Password,


            };
        }
    }
}
