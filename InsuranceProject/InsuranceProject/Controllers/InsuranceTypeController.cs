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
    public class InsuranceTypeController : ControllerBase
    {
        private IInsuranceTypeService _insuranceTypeService;
        public InsuranceTypeController(IInsuranceTypeService insuranceTypeService)
        {
            _insuranceTypeService = insuranceTypeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var insuranceTypeDTO = new List<InsuranceTypeDto>();
            var insuranceTypes = _insuranceTypeService.GetAll();
            if (insuranceTypes.Count > 0)
            {
                foreach (var insuranceType in insuranceTypes)
                {
                    insuranceTypeDTO.Add(ConvertToDTO(insuranceType));
                }
                return Ok(insuranceTypeDTO);
            }
            return BadRequest("Location not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var insuranceType = _insuranceTypeService.Get(id);
            if (insuranceType == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(insuranceType));
        }
        [HttpPost]
        public IActionResult Add(InsuranceTypeDto insuranceTypeDto)
        {
            var insuranceType = ConvertToModel(insuranceTypeDto);
            var insuranceTypeId = _insuranceTypeService.Add(insuranceType);
            if (insuranceTypeId == null)
                return BadRequest("Some errors Occurred");
            return Ok(insuranceTypeId);
        }
        [HttpPut]
        public IActionResult Update(InsuranceTypeDto insuranceTypeDto)
        {
            var insuranceTypeDTOToUpdate = _insuranceTypeService.Check(insuranceTypeDto.Id);
            if (insuranceTypeDTOToUpdate != null)
            {
                var updatedInsuranceType = ConvertToModel(insuranceTypeDto);
                var modifiedInsuranceType = _insuranceTypeService.Update(updatedInsuranceType);
                return Ok(ConvertToDTO(modifiedInsuranceType));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var insuranceType = _insuranceTypeService.Get(id);
            if (insuranceType != null)
            {
                _insuranceTypeService.Delete(insuranceType);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private InsuranceType ConvertToModel(InsuranceTypeDto insuranceTypeDto)
        {
            return new InsuranceType()
            {
                Id=insuranceTypeDto.Id,
                InsuranceTypeName=insuranceTypeDto.InsuranceTypeName,
                
                IsActive = true

            };
        }
        private InsuranceTypeDto ConvertToDTO(InsuranceType insuranceType)
        {
            return new InsuranceTypeDto()
            {
                Id= insuranceType.Id,
                InsuranceTypeName = insuranceType.InsuranceTypeName,
            };
        }
    }
}
