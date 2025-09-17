using BankingApp.Models;
using BankingApp.Repositories;


namespace BankingApp.Services
{
    public class ClientService:IClientService
    {
        private readonly IClientRepository _ClientRepository;

       public ClientService(IClientRepository clientRepository)
       {
           _ClientRepository = clientRepository;
       }
        public Client Add(Client Client)
        {
            return _ClientRepository.Add(Client);
        }
        public void Delete(int id)
        {
            _ClientRepository.Delete(id);
        }
        public IEnumerable<Client> GetAll()
        {
            return _ClientRepository.GetAll();
        }
        public Client GetById(int id)
        {
            return _ClientRepository.GetById(id);
        }
        public Client Update(Client Client)
        {
            return _ClientRepository.Update(Client);
        }
    }
}

