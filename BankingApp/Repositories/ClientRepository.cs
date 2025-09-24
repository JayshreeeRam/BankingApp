using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Clients
                .Include(c => c.Account)   // eager load account
                .Include(c => c.User)      // ✅ load User as well
                .AsNoTracking()            // better performance for read-only
                .ToList();
        }

        public Client? GetById(int id)
        {
            return _context.Clients
                .Include(c => c.Account)
                .Include(c => c.User)      // ✅ include User
                .FirstOrDefault(c => c.ClientId == id);
        }

        public Client Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client Update(int id, Client client)
        {
            var existing = _context.Clients
                                   .Include(c => c.Account)
                                   .FirstOrDefault(c => c.ClientId == id);

            if (existing == null) return null!;

            // Update only fields that are safe
            existing.Name = client.Name;
            existing.Address = client.Address;
            existing.BankId = client.BankId;
            existing.UserId = client.UserId;
            existing.VerificationStatus = client.VerificationStatus;
            existing.AccountType = client.AccountType;

            // If account exists, update reference
            if (client.Account != null)
                existing.Account = client.Account;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null) return false;

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return true;
        }
    }
}
