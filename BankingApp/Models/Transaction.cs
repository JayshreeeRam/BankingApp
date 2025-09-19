using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }


        [Required]
        public int AccountId { get; set; }
        public Account? Account { get; set; }    // Navigation property

        [Required]
        public TransactionType TransactionType { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
