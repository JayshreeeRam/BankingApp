using BankingApp.DTOs;
using BankingApp.Enums;

//using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BankingApp.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repo;
        private readonly Cloudinary _cloudinary;

        public DocumentService(IDocumentRepository repo, IOptions<CloudinarySettings> cloudSettings)
        {
            _repo = repo;
            var settings = cloudSettings.Value;

            var account = new CloudinaryDotNet.Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account)
            {
                Api = { Secure = true }
            };
        }

        public IEnumerable<Document> GetAll() => _repo.GetAll();

        public Document? GetById(int id) => _repo.GetById(id);

        public Document UploadDocument(IFormFile file, int uploadedByUserId, DocumentType type)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            if (_cloudinary == null)
                throw new Exception("Cloudinary is not initialized");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "BankingAppDocuments",
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = false,
                PublicId = Guid.NewGuid().ToString()
            };

            var uploadResult = _cloudinary.Upload(uploadParams);

            if (uploadResult == null || uploadResult.SecureUrl == null)
                throw new Exception("Cloudinary upload failed: " + (uploadResult?.Error?.Message ?? "Unknown error"));

            var document = new Document
            {
                FileName = file.FileName,
                FilePath = uploadResult.SecureUrl.AbsoluteUri,
                UploadDate = DateTime.UtcNow.ToLocalTime(),
                UploadedByUserId = uploadedByUserId,
                DocumentType = type,
                DocumentStatus = DocumentStatus.Pending
            };

            return _repo.Add(document);
        }


        public Document UpdateDocument(int id, Document document) => _repo.Update(id, document);

        public bool DeleteDocument(int id) => _repo.Delete(id);
    }
}
