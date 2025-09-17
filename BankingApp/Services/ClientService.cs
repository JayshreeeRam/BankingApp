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
         public bool Delete(int id)
        {
            var client = _ClientRepository.GetById(id);
            if (client == null)
              return false;
  
            _ClientRepository.Delete(id); // this returns void
            return true;
        }
        public IEnumerable<Client> GetAll()
        {
            return _ClientRepository.GetAll();
        }
        public Client GetById(int id)
        {
            return _ClientRepository.GetById(id);
        }
        public Client Update(int id ,Client Client)
        {
            return _ClientRepository.Update(Client);
        }
    }
}

