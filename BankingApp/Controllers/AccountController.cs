using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<AccountDto> GetById(int id)
        {
            var account = _service.GetById(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("client/{clientId}")]
        public ActionResult<IEnumerable<AccountDto>> GetByClientId(int clientId)
        {
            return Ok(_service.GetByClientId(clientId));
        }

        [HttpPost]
        public ActionResult<AccountDto> Create(CreateAccountDto accountDto)
        {
            var created = _service.Add(accountDto);
            return CreatedAtAction(nameof(GetById), new { id = created.AccountId }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<AccountDto> Update(int id, UpdateAccountDto accountDto)
        {
            var updated = _service.Update(id, accountDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
