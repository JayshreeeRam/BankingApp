using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _service;

        public BeneficiaryController(IBeneficiaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var b = _service.Find(id);
            if (b == null) return NotFound();
            return Ok(b);
        }

        [HttpPost]
        public IActionResult Add(Beneficiary beneficiary)
        {
            var created = _service.Add(beneficiary);
            return CreatedAtAction(nameof(GetById), new { id = created.BeneficiaryId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Beneficiary beneficiary)
        {
            var updated = _service.Update(id, beneficiary);
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
