using System.ComponentModel.DataAnnotations;

namespace BankingApp.DTOs
{
    public class CreateBeneficiaryDto
    {
        [Required]
        public int ClientId { get; set; }
    }
}
