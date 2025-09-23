
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankingContext _repo;

        public ClientRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _repo.Clients
                .Include(c => c.Bank)
                .Include(c => c.User)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Payments)
                .ToList();
        }

        public Client? GetById(int id)
        {
            return _repo.Clients
                .Include(c => c.Bank)
                .Include(c => c.User)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Payments)
                .FirstOrDefault(c => c.ClientId == id);
        }

        public Client Add(Client client)
        {
            _repo.Clients.Add(client);
            _repo.SaveChanges();
            return client;
        }

        public Client Update(int id, Client client)
        {
            var existing = _repo.Clients.Find(id);
            if (existing == null) return null!;

            // Update only the fields that are allowed to change
            existing.Name = client.Name;
            existing.Address = client.Address;
            existing.BankId = client.BankId;
            existing.UserId = client.UserId; // Only if you want to allow changing UserId
            existing.AccountNo = client.AccountNo;
            existing.VerificationStatus = client.VerificationStatus;
            existing.AccountType = client.AccountType;

            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var client = _repo.Clients.Find(id);
            if (client == null) return false;

            _repo.Clients.Remove(client);
            _repo.SaveChanges();
            return true;
        }
    }
}
