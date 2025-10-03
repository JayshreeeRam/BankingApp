using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles ="User,Admin,superAdmin")]
        public IActionResult GetAll()
        {
            var clients = _service.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,superAdmin")]
        public IActionResult GetById(int id)
        {
            var client = _service.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            var client = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = client.ClientId }, client);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Update(int id, [FromBody] UpdateClientDto dto)
        {
            var client = _service.Update(id, dto);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "superAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

        //  Approve client endpoint
        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
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
