using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

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

        public DateTime UploadDate { get; set; } = DateTime.UtcNow.ToLocalTime();

        [Required]
       
        public DocumentType DocumentType { get; set; }

        [Required]
        
        public DocumentStatus DocumentStatus { get; set; }

        
        
        [Required]
        public int UploadedByUserId { get; set; }

        [ForeignKey(nameof(UploadedByUserId))]
        [JsonIgnore] 
        public User? UploadedBy { get; set; } 
    }
}
