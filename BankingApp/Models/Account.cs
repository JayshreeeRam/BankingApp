using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public AccountType AccountType { get; set; }   // CURRENT / SAVINGS / SALARY

        [Required]
        public AccountStatus AccountStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        // 🔹 Relationship with Client (account holder)
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;

        // 🔹 Navigation property for transactions
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
