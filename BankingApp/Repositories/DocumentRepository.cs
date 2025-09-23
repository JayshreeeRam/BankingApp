using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{

    public class DocumentRepository : IDocumentRepository
    {
        private readonly BankingContext _repo;

        public DocumentRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Document> GetAll()
        {
            return _repo.Documents
                .Include(d => d.UploadedBy)
                .ToList();
        }

        public Document? GetById(int id)
        {
            return _repo.Documents
                .Include(d => d.UploadedBy)
                .FirstOrDefault(d => d.DocumentId == id);
        }

        public Document Add(Document document)
        {
            _repo.Documents.Add(document);
            _repo.SaveChanges();
            return document;
        }

        public Document? Update(int id, Document document)
        {
            var existing = _repo.Documents.Find(id);
            if (existing == null) return null;

            _repo.Entry(existing).CurrentValues.SetValues(document);
            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var document = _repo.Documents.Find(id);
            if (document == null) return false;

            _repo.Documents.Remove(document);
            _repo.SaveChanges();
            return true;
        }
    }
}
