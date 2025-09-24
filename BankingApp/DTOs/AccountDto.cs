using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public AccountType? AccountType { get; set; } 
        public AccountStatus? AccountStatus { get; set; } 
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
    }
}
