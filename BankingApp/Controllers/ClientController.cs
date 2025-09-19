using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        // GET: api/client
        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _service.GetAll();
            return Ok(clients);
        }

        // GET: api/client/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _service.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        // POST: api/client
        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ClientId }, created);
        }

        // PUT: api/client/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateClientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/client/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
