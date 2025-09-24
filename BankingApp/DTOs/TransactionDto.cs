using BankingApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }


        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

      

        // ✅ Instead of names/accounts, keep SenderId & ReceiverId
        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        // ✅ Optional: Include names in DTO for API responses (not required in Add/Update requests)
        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
    }
}
