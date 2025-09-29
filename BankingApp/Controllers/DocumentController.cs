using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAll()
        {
            var docs = _documentService.GetAll();
            return Ok(docs);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var doc = _documentService.GetById(id);
            if (doc == null) return NotFound();
            return Ok(doc);
        }

        [HttpPost("upload")]
        [Authorize(Roles = "User")]
        public IActionResult UploadDocument([FromForm] DocumentUploadDto dto)
        {
            Console.WriteLine("Received DTO:", dto.File);
            //if (dto == null)
            //    return BadRequest(new { message = "DTO is null" });

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest(new { message = "No file uploaded" });

            try
            {
                var doc = _documentService.UploadDocument(dto.File, dto.UserId, dto.DocumentType);
                Console.WriteLine("dto in controller:", doc);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading document:" + ex);
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteDocument(int id)
        {
            var deleted = _documentService.DeleteDocument(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
