using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Service;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var customerDTO = new List<CustomerDto>();
            var customers = _customerService.GetAll();
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    customerDTO.Add(ConvertToDTO(customer));
                }
                return Ok(customerDTO);
            }
            return BadRequest("Location not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerService.Get(id);
            if (customer == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(customer));
        }
        [HttpPost]
        public IActionResult Add(CustomerDto customerDto)
        {
            var customer = ConvertToModel(customerDto);
            var customerId = _customerService.Add(customer);
            if (customerId == null)
                return BadRequest("Some errors Occurred");
            return Ok(customerId);
        }
        [HttpPut]
        public IActionResult Update(CustomerDto customerDto)
        {
            var customerDTOToUpdate = _customerService.Check(customerDto.Id);
            if (customerDTOToUpdate != null)
            {
                var updatedCustomer = ConvertToModel(customerDto);
                var modifiedCustomer = _customerService.Update(updatedCustomer);
                return Ok(ConvertToDTO(modifiedCustomer));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.Get(id);
            if (customer != null)
            {
                _customerService.Delete(customer);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private Customer ConvertToModel(CustomerDto customerDto)
        {
            return new Customer()
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                DateOfBirth = customerDto.DateOfBirth,
                UserName = customerDto.UserName,
                Password = customerDto.Password,
                MobileNumber = customerDto.MobileNumber,
                Email = customerDto.Email,
                NomineeName = customerDto.NomineeName,
                NomineeRelation = customerDto.NomineeRelation,
                LocationId = customerDto.LocationId,
                AgentId = customerDto.AgentId,

                IsActive = true

            };
        }
        private CustomerDto ConvertToDTO(Customer customer)
        {
            return new CustomerDto()
            {
               Id= customer.Id,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               DateOfBirth = customer.DateOfBirth,
               UserName = customer.UserName,
               Password = customer.Password,
               MobileNumber = customer.MobileNumber,
               Email = customer.Email,
               NomineeName= customer.NomineeName,
               NomineeRelation= customer.NomineeRelation,
               LocationId = customer.LocationId,
               AgentId = customer.AgentId,

            };
        }
    }
}
