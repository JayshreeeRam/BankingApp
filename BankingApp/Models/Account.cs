using System.ComponentModel.DataAnnotations;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        public string AccountNumber { get; set; } = null!;
        public AccountType AccountType { get; set; }   // CURRENT / SAVINGS / SALARY
        public AccountStatus AccountStatus { get; set; }

        public decimal Balance { get; set; }


        //  public ICollection<Transaction>? Transactions { get; set; }
    }
}
