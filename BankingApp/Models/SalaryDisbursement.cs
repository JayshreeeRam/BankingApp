using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enum;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class SalaryDisbursement
    {
        [Key]
        public int DisbursementId { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? BatchId { get; set; }
    }
}
