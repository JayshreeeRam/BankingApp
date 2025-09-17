using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        // FK → Client
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;

        // FK → Beneficiary (nullable if you want SET NULL)
        public int? BeneficiaryId { get; set; }
        [ForeignKey(nameof(BeneficiaryId))]
        public Beneficiary? Beneficiary { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now.Date;

      public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
       
    }
}
