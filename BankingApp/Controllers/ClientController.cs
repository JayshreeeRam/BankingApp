using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_clientService.GetAllClients());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            var newClient = _clientService.CreateClient(client);
            return CreatedAtAction(nameof(GetById), new { id = newClient.ClientId }, newClient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Client client)
        {
            var updatedClient = _clientService.UpdateClient(id, client);
            if (updatedClient == null) return NotFound();
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _clientService.DeleteClient(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
