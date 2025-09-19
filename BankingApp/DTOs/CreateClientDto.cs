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
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        //[Required]
        //public string VerificationStatus { get; set; } = null!;

        [Required]
        public AccountType? AccountType { get; set; } 

        public string? AccountNo { get; set; }
    }
}