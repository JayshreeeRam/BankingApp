using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetAll()
        {
            IEnumerable<PaymentDto> payments = _service.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PaymentDto? payment = _service.GetById(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.Add(dto);
            // Return DTO after creation
            var createdDto = new PaymentDto
            {
                ClientId = created.ClientId,
                BeneficiaryId = created.BeneficiaryId,
                Amount = created.Amount,
                PaymentDate = created.PaymentDate,
                PaymentStatus = created.PaymentStatus
            };

            return CreatedAtAction(nameof(GetById), new { id = created.ClientId }, createdDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();

            var updatedDto = new PaymentDto
            {
                ClientId = updated.ClientId,
                BeneficiaryId = updated.BeneficiaryId,
                Amount = updated.Amount,
                PaymentDate = updated.PaymentDate,
                PaymentStatus = updated.PaymentStatus
            };

            return Ok(updatedDto);
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
