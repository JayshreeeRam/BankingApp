using BankingApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.DTOs
{
    public class SalaryDisbursementDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string? BatchId { get; set; }
    }
}
