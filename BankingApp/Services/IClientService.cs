using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        Client CreateClient(Client client);
        Client UpdateClient(int id, Client client);
        bool DeleteClient(int id);
    }
}
