using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public Client GetClientById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public Client CreateClient(Client client)
        {
            return _clientRepository.Add(client);
        }

        public Client UpdateClient(int id, Client client)
        {
            return _clientRepository.Update(id, client);
        }

        public bool DeleteClient(int id)
        {
            return _clientRepository.Delete(id);
        }
    }
}
