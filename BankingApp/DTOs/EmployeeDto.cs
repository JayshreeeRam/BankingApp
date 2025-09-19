using BankingApp.Models;
using System.Collections.Generic;

namespace BankingApp.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public int BankId { get; set; }
        public int ClientId { get; set; }

        // Optional: include SalaryDisbursements
        public ICollection<SalaryDisbursementDto>? SalaryDisbursements { get; set; }
    }
}