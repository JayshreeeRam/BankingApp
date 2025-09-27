using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserRole UserRole { get; set; } 
    }
}
