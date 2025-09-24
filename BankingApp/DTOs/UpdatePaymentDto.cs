using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class UpdatePaymentDto
    {
        public int? BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
