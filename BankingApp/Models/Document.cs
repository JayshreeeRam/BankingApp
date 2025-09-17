using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enum;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required, MaxLength(100)]
        public string FileName { get; set; } = null!;

        [Required]
        public string FilePath { get; set; } = null!;

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

      
        public DocumentType DocumentType { get; set; }

        public DocumentStatus DocumentStatus { get; set; }

        // FK → User
        public int UploadedByUserId { get; set; }
        [ForeignKey(nameof(UploadedByUserId))]
        public User UploadedBy { get; set; } = null!;
    }
}
