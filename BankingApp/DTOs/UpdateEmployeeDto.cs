namespace BankingApp.DTOs
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; } = null!;
        public int? BankId { get; set; }
        public int? ClientId { get; set; }
    }
}
