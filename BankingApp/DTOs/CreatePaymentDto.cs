namespace BankingApp.DTOs
{
    public class CreatePaymentDto
    {
        //public int PaymentId { get; set; }
        public int ClientId { get; set; }
        public int BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
