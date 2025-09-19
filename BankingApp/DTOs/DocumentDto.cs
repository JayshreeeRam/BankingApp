using BankingApp.Enum;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public DocumentType DocumentType { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public int UploadedByUserId { get; set; }
        public string? UploadedByUsername { get; set; } // optional
    }
}
