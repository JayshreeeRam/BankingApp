using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repositories
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
            return _context.Banks.ToList();
        }
        public Bank GetById(int id)
        {
            return _context.Banks
                .Include(b => b.Clients)
   
                .FirstOrDefault(b => b.BankId == id);

        }

        public Bank Add(Bank bank)
        {
            _context.Banks.Add(bank);
            _context.SaveChanges();
            return bank;
        }
        public Bank Update(Bank bank)
        {
            _context.Banks.Update(bank);
            _context.SaveChanges();
            return bank;
        }
        public void Delete(int id)
        {
            var bank = _context.Banks.Find(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
                _context.SaveChanges();
            }

        }
    }
}
