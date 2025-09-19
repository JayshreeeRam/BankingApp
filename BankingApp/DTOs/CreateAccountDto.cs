using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class CreateAccountDto
    {
        public string AccountNumber { get; set; }
        public AccountType? AccountType { get; set; }
        public AccountStatus? AccountStatus { get; set; } 
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
    }
}
