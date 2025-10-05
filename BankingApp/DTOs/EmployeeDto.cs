namespace BankingApp.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!; 
        public int BankId { get; set; }                  
        public string? BankName { get; set; }           
        public int SenderClientId { get; set; }         
        public string? SenderName { get; set; }          
        public int EmployeeClientId { get; set; }       
        public double Salary { get; set; }
        public string Department { get; set; } = null!;
    }
}
