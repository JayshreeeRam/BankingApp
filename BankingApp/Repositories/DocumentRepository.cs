using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{

    public class DocumentRepository : IDocumentRepository
    {
        private readonly BankingContext _context;

        public DocumentRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Document> GetAll()
        {
            return _context.Documents
                .Include(d => d.UploadedBy)
                .ToList();
        }

        public Document? GetById(int id)
        {
            return _context.Documents
                .Include(d => d.UploadedBy)
                .FirstOrDefault(d => d.DocumentId == id);
        }

        public Document Add(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
            return document;
        }

        public Document? Update(int id, Document document)
        {
            var existing = _context.Documents.Find(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(document);
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null) return false;

            _context.Documents.Remove(document);
            _context.SaveChanges();
            return true;
        }
    }
}
