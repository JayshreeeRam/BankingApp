namespace BankingApp.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TransactionStatus { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
    }
}
