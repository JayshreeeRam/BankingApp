using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _service.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

            [HttpPost]
            public IActionResult Add([FromBody] CreateEmployeeDto dto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var created = _service.Add(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.EmployeeId }, created);
            }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _service.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
