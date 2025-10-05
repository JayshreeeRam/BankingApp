using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IBankService _service;

        public BankController(IBankService service)
        {
            _service = service;
        }

        // GET: api/bank
        [HttpGet]
        //[Authorize(Roles = "SuperAdmin")]
        public ActionResult<IEnumerable<BankDto>> GetAll()
        {
            var banks = _service.GetAll();
            return Ok(banks);
        }

        // GET: api/bank/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult<BankDto> GetById(int id)
        {
            var bank = _service.GetById(id);
            if (bank == null) return NotFound();
            return Ok(bank);
        }

        // POST: api/bank
        [HttpPost]
        //[Authorize(Roles = "SuperAdmin")]
        public ActionResult<BankDto> Create(CreateBankDto bankDto)
        {
            var created = _service.Add(bankDto);
            return CreatedAtAction(nameof(GetById), new { id = created.BankId }, created);
        }

        // PUT: api/bank/{id}
        //[HttpPut("{id}")]
        //public ActionResult<BankDto> Update(int id, BankDto bankDto)
        //{
        //    var updated = _service.Update(id, bankDto);
        //    if (updated == null) return NotFound();
        //    return Ok(updated);
        //}

        // DELETE: api/bank/{id}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var deleted = _service.Delete(id);
        //    if (!deleted) return NotFound();
        //    return NoContent();
        //}
    }
}
