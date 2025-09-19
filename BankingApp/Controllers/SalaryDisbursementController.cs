using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var salary = _service.GetById(id);
            if (salary == null) return NotFound();
            return Ok(salary);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SalaryDisbursementDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DisbursementId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SalaryDisbursementDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
