using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryDisbursementController : ControllerBase
    {
        private readonly ISalaryDisbursementService _service;

        public SalaryDisbursementController(ISalaryDisbursementService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var disbursement = _service.Find(id);
            if (disbursement == null) return NotFound();
            return Ok(disbursement);
        }

        [HttpPost]
        public IActionResult Add(SalaryDisbursement disbursement)
        {
            var created = _service.Add(disbursement);
            return CreatedAtAction(nameof(GetById), new { id = created.DisbursementId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SalaryDisbursement disbursement)
        {
            var updated = _service.Update(id, disbursement);
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
