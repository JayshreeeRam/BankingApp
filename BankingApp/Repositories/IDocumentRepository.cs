using BankingApp.Models;

namespace BankingApp.Repositories
{

    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();
        Document? GetById(int id);
        Document Add(Document document);
        Document? Update(int id, Document document);
        bool Delete(int id);
    }
}
