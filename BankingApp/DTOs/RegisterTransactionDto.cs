namespace BankingApp.DTOs
{
    public class RegisterTransactionDto
    {
        public int AccountId { get; set; }
        public string TransactionType { get; set; } = string.Empty;  // Deposit / Withdrawal etc.
        public decimal Amount { get; set; }
        public string TransactionStatus { get; set; } = "Pending";  // Default on create
    }
}
