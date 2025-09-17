namespace BankingApp.DTOs
{
    public class UpdateTransactionDto
    {
        public decimal? Amount { get; set; }
        public string? TransactionType { get; set; }
        public string? TransactionStatus { get; set; }
    }
}
