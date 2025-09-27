using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
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
            var transaction = _service.GetById(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet("account/{accountId}")]
        public IActionResult GetByAccount(int accountId)
        {
            return Ok(_service.GetByAccount(accountId));
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetTransactionsForUser(int userId)
        {
            var transactions = _service.GetByClientId(userId); // Normal function
            return Ok(transactions);
        }


        //[HttpPost]
        //public IActionResult Add([FromBody] TransactionDto dto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var created = _service.Add(dto);
        //    return CreatedAtAction(nameof(GetById), new { id = created.TransactionId }, created);
        //}


        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] TransactionDto dto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var updated = _service.Update(id, dto);
        //    if (updated == null) return NotFound();
        //    return Ok(updated);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var success = _service.Delete(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}
    }
}
