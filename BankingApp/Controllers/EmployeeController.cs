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
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _service.GetById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public IActionResult Add([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EmployeeId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _service.Update(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id); // Call the method without assigning to a variable
            return NoContent(); // Return NoContent directly
        }
    }
}
