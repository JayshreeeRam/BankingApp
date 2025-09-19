namespace BankingApp.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = null!;
        public int? BankId { get; set; }
        public int? ClientId { get; set; }
    }
}
