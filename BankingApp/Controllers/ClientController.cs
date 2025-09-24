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

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _service.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _service.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            var client = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = client.ClientId }, client);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateClientDto dto)
        {
            var client = _service.Update(id, dto);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // ? Approve client endpoint
        [HttpPost("{id}/approve")]
        public IActionResult Approve(int id)
        {
            try
            {
                var client = _service.ApproveClient(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
