using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        Client Add(Client Client);
        Client Update( int id ,Client client);
        bool Delete(int id);
    }
}
