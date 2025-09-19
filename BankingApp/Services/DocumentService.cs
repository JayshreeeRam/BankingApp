using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BankingApp.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repo;
        private readonly Cloudinary _cloudinary;

        public DocumentService(IDocumentRepository repo, Cloudinary cloudinary)
        {
            _repo = repo;
            _cloudinary = cloudinary;
        }

        public IEnumerable<DocumentDto> GetAll()
        {
            return _repo.GetAll().Select(MapToDto).ToList();
        }

        public DocumentDto? GetById(int id)
        {
            var doc = _repo.GetById(id);
            return doc == null ? null : MapToDto(doc);
        }

        public DocumentDto Add(DocumentDto dto)
        {
            var doc = new Document
            {
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                UploadDate = dto.UploadDate,
                DocumentType = dto.DocumentType,
                DocumentStatus = dto.DocumentStatus,
                UploadedByUserId = dto.UploadedByUserId
            };

            var created = _repo.Add(doc);
            return MapToDto(created);
        }

        public DocumentDto? Update(int id, DocumentDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            existing.FileName = dto.FileName;
            existing.FilePath = dto.FilePath;
            existing.UploadDate = dto.UploadDate;
            existing.DocumentType = dto.DocumentType;
            existing.DocumentStatus = dto.DocumentStatus;
            existing.UploadedByUserId = dto.UploadedByUserId;

            var updated = _repo.Update(id, existing);
            return updated == null ? null : MapToDto(updated);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        // ✅ Upload document via Cloudinary and return DocumentDto
        public async Task<DocumentDto> UploadDocument(DocumentUploadDto dto)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(dto.File.FileName, dto.File.OpenReadStream()),
                Folder = "documents"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            var document = new Document
            {
                UploadedByUserId = dto.UserId,
                FilePath = uploadResult.SecureUrl.AbsoluteUri,
                FileName = dto.File.FileName,
                UploadDate = DateTime.Now,
                DocumentStatus = Enums.DocumentStatus.Pending,
                DocumentType =dto.DocumentType  
            };

            var created = _repo.Add(document);
            return MapToDto(created);
        }

        // Helper to map Document → DocumentDto
        private DocumentDto MapToDto(Document doc)
        {
            return new DocumentDto
            {
                DocumentId = doc.DocumentId,
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                UploadDate = doc.UploadDate,
                DocumentType = doc.DocumentType,
                DocumentStatus = doc.DocumentStatus,
                UploadedByUserId = doc.UploadedByUserId,
                UploadedByUsername = doc.UploadedBy?.Username
            };
        }
    }
}
