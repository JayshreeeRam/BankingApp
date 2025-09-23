namespace BankingApp.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!; // Uncommented the Name property
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public double Salary { get; set; }
    }
}
