using BankingApp.Enum;

namespace BankingApp.DTOs
{
    public class DocumentUploadDto
    {
        public IFormFile File { get; set; } = null!;
        public int UserId { get; set; }
        public DocumentType DocumentType { get; set; } =DocumentType.Other;
    }


}
