using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _service;

        public BeneficiaryController(IBeneficiaryService service)
        {
            _service = service;
        }

        // GET: api/Beneficiary
        [HttpGet]
        public ActionResult<IEnumerable<BeneficiaryDto>> GetAll()
        {
            var beneficiaries = _service.GetAll();
            return Ok(beneficiaries);
        }

        // GET: api/Beneficiary/5
        [HttpGet("{id}")]
        public ActionResult<BeneficiaryDto> GetById(int id)
        {
            var beneficiary = _service.GetById(id);
            if (beneficiary == null)
                return NotFound();

            return Ok(beneficiary);
        }

        // POST: api/Beneficiary
        // Create using only clientId
        [HttpPost]
        public ActionResult<BeneficiaryDto> Create([FromBody] CreateBeneficiaryDto dto)
        {
            try
            {
                var created = _service.Add(dto.ClientId);
                return CreatedAtAction(nameof(GetById), new { id = created.BeneficiaryId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Beneficiary/5
        //[HttpPut("{id}")]
        //public ActionResult<BeneficiaryDto> Update(int id, [FromBody] BeneficiaryDto dto)
        //{
        //    try
        //    {
        //        var updated = _service.Update(id, dto);
        //        if (updated == null)
        //            return NotFound();

        //        return Ok(updated);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        // DELETE: api/Beneficiary/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
