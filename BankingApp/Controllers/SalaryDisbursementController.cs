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
        public IActionResult Add(SalaryDisbursementDto dto)
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

        // Approve salary (and create Transaction with proper names)
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
    }
}
