using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _service;

        public BeneficiaryController(IBeneficiaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BeneficiaryDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<BeneficiaryDto> GetById(int id)
        {
            var beneficiary = _service.GetById(id);
            if (beneficiary == null) return NotFound();
            return Ok(beneficiary);
        }

        [HttpPost]
        public ActionResult<BeneficiaryDto> Create(BeneficiaryDto dto)
        {
            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BeneficiaryId }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<BeneficiaryDto> Update(int id, BeneficiaryDto dto)
        {
            var updated = _service.Update(id, dto);
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
