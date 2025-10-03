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
        //[Authorize(Roles = "Admin,SuperAdmin")]
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
        //[Authorize(Roles = "Admin,User,Client")]
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
        //[HttpPost("{id}/approve")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Approve(int id)
        //{
        //    var approvedPayment = _service.ApprovePayment(id);

        //    if (approvedPayment == null)
        //    {
        //        Console.WriteLine("Approval failed: payment not found or insufficient funds");

        //        return BadRequest("Payment cannot be approved. Either not found or insufficient funds.");
        //    }
        //    return Ok(approvedPayment);
        //}

        [HttpPost("approve/{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            try
            {
                Console.WriteLine($"[Controller] Approving id: {id}");

                var approvedPayment = _service.ApprovePayment(id);

                Console.WriteLine($"[Approve] Payment with id {id} approved successfully in controller.");
                return Ok(approvedPayment);
            }
            catch (ArgumentException ex)
            {
                // For not found scenarios
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // For business rule violations
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // For unexpected exceptions
                Console.WriteLine($"[Approve] Unexpected error: {ex}");
                return StatusCode(500, new { message = "An unexpected error occurred while approving the payment." });
            }
        }

        [HttpPost("reject/{id}")]
        public IActionResult Reject(int id, [FromBody] RejectRequest request)
        {
            try
            {
                Console.WriteLine($"[Controller] Rejecting id: {id} with remark: {request.Remark}");

                var rejectedPayment = _service.RejectPayment(id, request.Remark);

                Console.WriteLine($"[Reject] Payment with id {id} rejected successfully.");
                return Ok(rejectedPayment);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Reject] Unexpected error: {ex}");
                return StatusCode(500, new { message = "An unexpected error occurred while rejecting the payment." });
            }
        }

        public class RejectRequest
        {
            public string Remark { get; set; }
        }

    }
}
