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
    public class PolicyClaimController : ControllerBase
    {
        private IPolicyClaimService _policyClaimService;
        public PolicyClaimController(IPolicyClaimService policyClaimService)
        {
            _policyClaimService = policyClaimService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var policyClaimDTO = new List<PolicyClaimDto>();
            var policyClaims = _policyClaimService.GetAll();
            if (policyClaims.Count > 0)
            {
                foreach (var policyClaim in policyClaims)
                {
                    policyClaimDTO.Add(ConvertToDTO(policyClaim));
                }
                return Ok(policyClaimDTO);
            }
            return BadRequest("Contacts not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var policyClaim = _policyClaimService.Get(id);
            if (policyClaim == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(policyClaim));
        }
        [HttpPost]
        public IActionResult Add(PolicyClaimDto policyClaimDto)
        {
            var policyClaim = ConvertToModel(policyClaimDto);
            var policyClaimId = _policyClaimService.Add(policyClaim);
            if (policyClaimId == null)
                return BadRequest("Some errors Occurred");
            return Ok(policyClaimId);
        }
        [HttpPut]
        public IActionResult Update(PolicyClaimDto policyClaimDto)
        {
            var policyClaimDTOToUpdate = _policyClaimService.Check(policyClaimDto.Id);
            if (policyClaimDTOToUpdate != null)
            {
                var updatedPolicyClaim = ConvertToModel(policyClaimDto);
                var modifiedPolicyClaim = _policyClaimService.Update(updatedPolicyClaim);
                return Ok(ConvertToDTO(modifiedPolicyClaim));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var policyClaim = _policyClaimService.Get(id);
            if (policyClaim != null)
            {
                _policyClaimService.Delete(policyClaim);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private PolicyClaim ConvertToModel(PolicyClaimDto policyClaimDto)
        {
            return new PolicyClaim()
            {
                Id=policyClaimDto.Id,
                InsurancePlanId=policyClaimDto.InsurancePlanId,
                CustomerId=policyClaimDto.CustomerId,
                BankName=policyClaimDto.BankName,
                WithdrawalAmount=policyClaimDto.WithdrawalAmount,
                WithdrawalDate=policyClaimDto.WithdrawalDate,
                
                IsActive = true

            };
        }
        private PolicyClaimDto ConvertToDTO(PolicyClaim policyClaim)
        {
            return new PolicyClaimDto()
            {
                Id = policyClaim.Id,
                InsurancePlanId = policyClaim.InsurancePlanId,
                CustomerId = policyClaim.CustomerId,
                BankName = policyClaim.BankName,
                WithdrawalAmount = policyClaim.WithdrawalAmount,
                WithdrawalDate = policyClaim.WithdrawalDate,

            };
        }
    }
}
