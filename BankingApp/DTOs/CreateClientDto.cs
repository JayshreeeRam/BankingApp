using System.ComponentModel.DataAnnotations;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class CreateClientDto
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
    }
}