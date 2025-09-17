using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        Client Add(Client Client);
        Client Update(Client Client);
        void Delete(int id);
    }
}
