using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        Client Add(Client Client);
        Client Update(Client Client);
        void Delete(int id);
    }
}
