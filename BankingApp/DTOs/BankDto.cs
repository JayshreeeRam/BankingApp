    namespace BankingApp.DTOs
    {
    public class BankDto
    {
        public int BankId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string IFSCCODE { get; set; } = null!;
    }
}
