using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult GetAll()
        {
            var payments = _service.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var payment = _service.GetById(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        // Use CreatePaymentDto for request
        [HttpPost]
        [Authorize(Roles = "Admin,User,Client")]
        public IActionResult Add([FromBody] CreatePaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ClientId }, created);
        }

        

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    bool success = _service.Delete(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}

        // approve payment & create transactions
        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            var approvedPayment = _service.ApprovePayment(id);

            if (approvedPayment == null)
                return BadRequest("Payment cannot be approved. Either not found or insufficient funds.");

            return Ok(approvedPayment);
        }
    }
}
