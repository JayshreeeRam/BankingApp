using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_service.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = _service.GetEmployeeById(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var emp = _service.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetById), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            var updated = _service.UpdateEmployee(id, employee);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.DeleteEmployee(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
