using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Services
{
    public interface IDocumentService
    {
        IEnumerable<Document> GetAll();
        Document? GetById(int id);
        Document UploadDocument(IFormFile file, int uploadedByUserId, DocumentType type);
        Document UpdateDocument(int id, Document document);
        bool DeleteDocument(int id);
    }
}
