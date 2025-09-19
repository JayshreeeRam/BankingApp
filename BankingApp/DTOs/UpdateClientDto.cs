using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class UpdateClientDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? BankId { get; set; }
        public int? UserId { get; set; }
        public AccountStatus VerificationStatus { get; set; } = AccountStatus.Pending;
        public AccountType AccountType { get; set; }
        public string? AccountNo { get; set; }
    }
}
