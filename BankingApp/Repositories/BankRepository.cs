using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankingContext _context;

        public BankRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Bank> GetAll()
        {
            return _context.Banks.AsNoTracking().ToList();
        }

        public Bank? GetById(int id)
        {
            return _context.Banks.Find(id);
        }

        public Bank Add(Bank bank)
        {
            _context.Banks.Add(bank);
            _context.SaveChanges();
            return bank;
        }

        public Bank Update(int id, Bank bank)
        {
            var existing = _context.Banks.Find(id);
            if (existing == null) return null!;

            existing.Name = bank.Name;
            existing.Address = bank.Address;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var bank = _context.Banks.Find(id);
            if (bank == null) return false;

            _context.Banks.Remove(bank);
            _context.SaveChanges();
            return true;
        }
    }
}
