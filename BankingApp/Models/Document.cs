using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Enum;
using BankingApp.Enums;
using System.Text.Json.Serialization;

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

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public DocumentStatus DocumentStatus { get; set; }

  
        // FK → User
        [Required]
        public int UploadedByUserId { get; set; }

        [ForeignKey(nameof(UploadedByUserId))]
        [JsonIgnore] // Prevent cycles during serialization
        public User UploadedBy { get; set; } = null!;
    }
}
