using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class SalaryDisbursement
    {
        [Key]
        public int DisbursementId { get; set; }

        [Required]
        public int EmployeeId { get; set; }  // Nullable
        [ForeignKey(nameof(EmployeeId))]

        
        public Employee? Employee { get; set; }  // Nullable navigation

        [Required]
        public int ClientId { get; set; }  // Nullable
        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }  // Nullable navigation

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public int BatchId { get; set; }
    }

}
