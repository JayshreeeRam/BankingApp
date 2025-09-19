using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingContext _context;

        public AccountRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.Include(a => a.Client).ToList();
        }

        public Account? GetById(int id)
        {
            return _context.Accounts.Include(a => a.Client)
                                    .FirstOrDefault(a => a.AccountId == id);
        }

        public IEnumerable<Account> GetByClientId(int clientId)
        {
            return _context.Accounts.Where(a => a.ClientId == clientId).ToList();
        }

        public Account Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account? Update(int id, Account account)
        {
            var existing = _context.Accounts.Find(id);
            if (existing == null) return null;

            existing.AccountNumber = account.AccountNumber;
            existing.AccountType = account.AccountType;
            existing.AccountStatus = account.AccountStatus;
            existing.Balance = account.Balance;
            //existing.ClientId = account.ClientId;

            _context.SaveChanges();
            return existing;
        }




        public bool Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return true;
        }
    }
}
