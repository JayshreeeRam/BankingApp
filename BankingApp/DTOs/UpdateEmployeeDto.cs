namespace BankingApp.DTOs
{
    public class UpdateEmployeeDto
    {
        public int SenderClientId { get; set; }
        public int EmployeeClientId { get; set; }
        public double Salary { get; set; }

    }
}
