using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryDisbursementController : ControllerBase
    {
        private readonly ISalaryDisbursementService _salaryService;

        public SalaryDisbursementController(ISalaryDisbursementService salaryService)
        {
            _salaryService = salaryService;
        }

        // Get all salary disbursements
        [HttpGet]
        public IActionResult GetAll() => Ok(_salaryService.GetAll());

        // Get by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var salary = _salaryService.GetById(id);
            if (salary == null) return NotFound();
            return Ok(salary);
        }

        // Add new salary disbursement
        [HttpPost]
        public IActionResult Add(CreateSalaryDisbursementDto dto)
        {
            try
            {
                var result = _salaryService.Add(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateSalary(int id, [FromBody] SalaryDisbursementDto dto)
        {
            if (id != dto.DisbursementId)
                return BadRequest("ID mismatch");

            try
            {
                var result = _salaryService.Update(id, dto); // Your service logic
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // Approve salary 
        [HttpPost("approve/{id}")]
        public IActionResult ApproveSalary(int id)
        {
            var result = _salaryService.ApproveSalary(id);
            if (result == null) return BadRequest("Cannot approve salary. Ensure employee and client exist and client has sufficient balance.");
            return Ok(result);
        }



        // Approve all salaries in a batch
        [HttpPost("approveBatch/{batchId}")]
        public IActionResult ApproveSalaryByBatch(int batchId)
            => Ok(_salaryService.ApproveSalaryByBatch(batchId));


        [HttpPost("reject/{id}")]
        public IActionResult RejectSalary(int id)
        {
            var salary = _salaryService.GetById(id);
            if (salary == null)
                return NotFound($"Salary disbursement with id {id} not found.");

            if (salary.Status == PaymentStatus.Rejected)
                return BadRequest("Salary disbursement already rejected.");

            salary.Status = PaymentStatus.Rejected;  // assign enum value, not string

            var updatedSalary = _salaryService.Update(id, salary); // pass id and DTO

            if (updatedSalary == null)
                return BadRequest("Failed to reject salary disbursement in controller.");

            return Ok(updatedSalary);
        }


    }
}
