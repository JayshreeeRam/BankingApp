using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
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
            var user = _service.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var success = _service.Delete(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}
    }
}
