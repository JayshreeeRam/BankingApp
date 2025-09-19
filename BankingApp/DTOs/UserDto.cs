using System.ComponentModel.DataAnnotations;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class UserDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; } = null!;

        public UserRole UserRole { get; set; }

        //public int? ClientId { get; set; }
        //public ClientDto? Client { get; set; }
    }
}
