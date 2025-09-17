using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _service;

        public BankController(IBankService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var bank = _service.Find(id);
            if (bank == null) return NotFound();
            return Ok(bank);
        }

        [HttpPost]
        public IActionResult Add(Bank bank)
        {
            var created = _service.Add(bank);
            return CreatedAtAction(nameof(GetById), new { id = created.BankId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Bank bank)
        {
            var updated = _service.Update(id, bank);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
