using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IDocumentService
    {
        IEnumerable<DocumentDto> GetAll();
        DocumentDto? GetById(int id);
        DocumentDto Add(DocumentDto dto);
        DocumentDto? Update(int id, DocumentDto dto);
        bool Delete(int id);
        Task<DocumentDto> UploadDocument(DocumentUploadDto dto);

    }
}
