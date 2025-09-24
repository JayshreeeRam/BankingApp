using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client? GetById(int id);
        Client Add(Client client);
        Client Update(int id, Client client);
        bool Delete(int id);
    }
}
