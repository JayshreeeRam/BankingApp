using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; 
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }
        public Account? Account { get; set; } 

        [Required]
        public TransactionType TransactionType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        [JsonIgnore]  
        public Client? Sender { get; set; } = null!;

        [Required]
        public int ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        [JsonIgnore]
        public Client? Receiver { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string SenderName { get; set; } = null!;    

        [Required]
        [MaxLength(100)]
        public string ReceiverName { get; set; } = null!;  

        public TransactionStatus TransactionStatus { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
