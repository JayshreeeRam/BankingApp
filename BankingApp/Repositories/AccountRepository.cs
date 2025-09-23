using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingContext _repo;

        public AccountRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _repo.Accounts.Include(a => a.Client).ToList();
        }

        public Account? GetById(int id)
        {
            return _repo.Accounts.Include(a => a.Client)
                                    .FirstOrDefault(a => a.AccountId == id);
        }

        public Account? GetByClientId(int clientId)
        {
            // Assuming one account per client
            return _repo.Accounts.Include(a => a.Client)
                                 .FirstOrDefault(a => a.ClientId == clientId);
        }


        public Account Add(Account account)
        {
            _repo.Accounts.Add(account);
            _repo.SaveChanges();
            return account;
        }

        public Account? Update(int id, Account account)
        {
            var existing = _repo.Accounts.Find(id);
            if (existing == null) return null;

            existing.AccountNumber = account.AccountNumber;
            existing.AccountType = account.AccountType;
            existing.AccountStatus = account.AccountStatus;
            existing.Balance = account.Balance;
            //existing.ClientId = account.ClientId;

            _repo.SaveChanges();
            return existing;
        }




        public bool Delete(int id)
        {
            var account = _repo.Accounts.Find(id);
            if (account == null) return false;

            _repo.Accounts.Remove(account);
            _repo.SaveChanges();
            return true;
        }
    }
}
