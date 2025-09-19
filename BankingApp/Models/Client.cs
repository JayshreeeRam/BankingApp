using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string? Address { get; set; }

        // FK → Bank
        [Required]
        public int BankId { get; set; }
        [ForeignKey(nameof(BankId))]
        public Bank Bank { get; set; } = null!;

        // FK → User
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string AccountNo { get; set; } = null!;

        [Required]
        public AccountStatus VerificationStatus { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        // Navigation collections
        public ICollection<Beneficiary>? Beneficiaries { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }
    }
}
