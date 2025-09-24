
using System.Text.Json.Serialization;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class DocumentUploadDto
    {
        public IFormFile File { get; set; }
        public int UserId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType DocumentType { get; set; } 
    }


}
