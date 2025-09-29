using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        // FK → Client (who initiates the payment)
        [Required]
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;

        // FK → Beneficiary(who gets the payment)
        [Required]
        public int BeneficiaryId { get; set; }
        [ForeignKey(nameof(BeneficiaryId))]
        public Beneficiary Beneficiary { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }  

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

      
      

       
    }
}
