using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankingContext _context;

        public ClientRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _context.Clients.Find(id);
        }

        public Client Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client Update(int id, Client client)
        {
            var existingClient = _context.Clients.Find(id);
            if (existingClient != null)
            {
                existingClient.Name = client.Name;
                existingClient.Address = client.Address;
                // Add other fields if you have them
                _context.SaveChanges();
            }
            return existingClient;
        }

        public bool Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
