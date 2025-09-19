using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentDto>), 200)]
        public IActionResult GetAll()
        {
            var documents = _service.GetAll();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var doc = _service.GetById(id);
            if (doc == null) return NotFound();
            return Ok(doc);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DocumentDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult Add([FromBody] DocumentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DocumentId }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DocumentDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] DocumentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id)) return NotFound();
            return NoContent();
        }

        // File upload endpoint
        [HttpPost("upload")]
        [ProducesResponseType(typeof(DocumentDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required.");

            var document = await _service.UploadDocument(dto);
            return Ok(document);
        }
    }
}
