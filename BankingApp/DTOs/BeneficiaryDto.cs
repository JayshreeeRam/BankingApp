namespace BankingApp.DTOs
{
    public class BeneficiaryDto
    {
        public int BeneficiaryId { get; set; }
        public string BankName { get; set; } = null!;
        public string? AccountNo { get; set; }
        public string IFSCCode { get; set; } = null!; 
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
    }
}
