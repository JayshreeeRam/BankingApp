using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class ClientRepository: IClientRepository
    {
        private readonly BankingContext _context;
        
        public ClientRepository(BankingContext context)
        {
            _context = context;
        }


        Client IClientRepository.Add(Client Client)
        {
            _context.Clients.Add(Client);
            _context.SaveChanges();
            return Client;
        }
        void IClientRepository.Delete(int id)
        {
            var Client = _context.Clients.Find(id);
            if (Client != null)
            {
                _context.Clients.Remove(Client);
                _context.SaveChanges();
            }
        }
        IEnumerable<Client> IClientRepository.GetAll()
        {
            return _context.Clients.ToList();
        }
        Client IClientRepository.GetById(int id)
        {
            return _context.Clients.Find(id);
        }
        Client IClientRepository.Update(Client Client)
        {
            var existingClient = _context.Clients.Find(Client.ClientId);
            if (existingClient != null)
            {
                existingClient.Address= Client.Address;
                _context.SaveChanges();
            }
                return existingClient;
          
               
        }

 }
}