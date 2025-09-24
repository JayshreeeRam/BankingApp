
using System.Text.Json.Serialization;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentStatus DocumentStatus { get; set; }
        public int UploadedByUserId { get; set; }
        public string? UploadedByUsername { get; set; } // optional
        public string? PublicId { get; set; }
    }
}
