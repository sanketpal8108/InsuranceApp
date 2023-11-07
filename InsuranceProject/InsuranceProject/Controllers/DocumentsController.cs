using InsuranceDay1.Models;
using InsuranceProject.DTO;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace InsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IDocumentService _documentService;
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpGet("getAll")]
        public IActionResult Get()
        {
            var documentDTO = new List<DocumentsDto>();
            var documents = _documentService.GetAll();
            if (documents.Count > 0)
            {
                foreach (var document in documents)
                {
                    documentDTO.Add(ConvertToDTO(document));
                }
                return Ok(documentDTO);
            }
            return BadRequest("Location not found");
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var document = _documentService.Get(id);
            if (document == null)
            {
                return BadRequest("Contacts not found");
            }
            return Ok(ConvertToDTO(document));
        }
        [HttpPost("Add")]
        public IActionResult Add(DocumentsDto documentDto)
        {
            var document = ConvertToModel(documentDto);
            var documentId = _documentService.Add(document);
            if (documentId == null)
                return BadRequest("Some errors Occurred");
            return Ok(documentId);
        }
        [HttpPut("Update")]
        public IActionResult Update(DocumentsDto documentDto)
        {
            var documentDTOToUpdate = _documentService.Check(documentDto.Id);
            if (documentDTOToUpdate != null)
            {
                var updatedDocument = ConvertToModel(documentDto);
                var modifiedDocument = _documentService.Update(updatedDocument);
                return Ok(ConvertToDTO(modifiedDocument));
            }
            return BadRequest("No contact found to update");
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var document = _documentService.Get(id);
            if (document != null)
            {
                _documentService.Delete(document);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private Documents ConvertToModel(DocumentsDto documentDto)
        {
            return new Documents()
            {
                Id = documentDto.Id,
                DocumentType = documentDto.DocumentType,
                //DocumentInformation = documentDto.DocumentInformation,
                DocumentData = documentDto.DocumentData,
                CustomerId = documentDto.CustomerId,
                IsActive = true

            };
        }
        private DocumentsDto ConvertToDTO(Documents document)
        {
            return new DocumentsDto()
            {
                Id= document.Id,
                DocumentType = document.DocumentType,
                DocumentData= document.DocumentData,
                //DocumentInformation = document.DocumentInformation,
                CustomerId = document.CustomerId,

            };
        }
    }
}
