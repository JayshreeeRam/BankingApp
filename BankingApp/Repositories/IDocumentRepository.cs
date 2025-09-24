using BankingApp.Models;
using System.Collections.Generic;

namespace BankingApp.Repository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();
        Document? GetById(int id);
        Document Add(Document doc);
        Document? Update(int id, Document doc);
        bool Delete(int id);
    }
}
