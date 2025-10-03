using BankingApp.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public ClientController(IClientService service, IEmailService emailService, IUserService userService)
        {
            _service = service;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpGet]
        //[Authorize(Roles ="User,Admin,superAdmin")]
        public IActionResult GetAll()
        {
            var clients = _service.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,superAdmin")]
        public IActionResult GetById(int id)
        {
            var client = _service.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            var client = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = client.ClientId }, client);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Update(int id, [FromBody] UpdateClientDto dto)
        {
            var client = _service.Update(id, dto);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "superAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // Approve client endpoint
        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            try
            {
                var client = _service.ApproveClient(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Reject client endpoint with remark
        [HttpPost("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Reject(int id, [FromBody] RejectClientRequest request)
        {
            try
            {
                var client = _service.RejectClient(id, request.Remark);

                // Send rejection email using UserService to get email
                if (client != null)
                {
                    // Get user email using UserService
                    var user = _userService.GetById(client.UserId);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        var emailSent = _emailService.SendRejectionEmail(user.Email, client.Name, request.Remark);
                        if (!emailSent)
                        {
                            // Log email failure but don't fail the request
                            Console.WriteLine($"Failed to send rejection email to client {user.Email}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No email found for client {client.ClientId} with UserId {client.UserId}");
                    }
                }

                return Ok(client);
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
                return StatusCode(500, new { message = "An unexpected error occurred while rejecting the client." });
            }
        }
    }

    public class RejectClientRequest
    {
        public string Remark { get; set; }
    }
}