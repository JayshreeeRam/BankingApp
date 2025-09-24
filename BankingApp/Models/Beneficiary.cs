using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Beneficiary
    {
        [Key]
        public int BeneficiaryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BankName { get; set; } = null!;

      

        [MaxLength(20)]
        public string? AccountNo { get; set; }

        [Required]

        public string? IFSCCode { get; set; } = "SBIN001250";


        // FK → Client
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }
    }
}
