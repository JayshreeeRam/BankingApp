using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class UpdateAccountDto
    {
        //public string AccountNumber { get; set; } 
        public AccountType? AccountType { get; set; } 
        public AccountStatus? AccountStatus { get; set; }
        public decimal? Balance { get; set; }
    }
}
