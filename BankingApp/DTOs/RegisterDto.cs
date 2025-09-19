using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public UserRole UserRole { get; set; }
    }
}
