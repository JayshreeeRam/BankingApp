using BankingApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.DTOs
{
    public class TransactionDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string? ReferenceId { get; set; }
        public string? Description { get; set; }
    }
}
