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
        [MaxLength(11)]
        [MinLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Account number must be 11 digits.")]

        public string AccountNumber { get; private set; } = null!;

        [Required]
        public AccountType AccountType { get; set; }  

        [Required]
        public AccountStatus AccountStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;

       public ICollection<Transaction>? Transactions { get; set; }

        public Account(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        private Account() { }
    }
}
