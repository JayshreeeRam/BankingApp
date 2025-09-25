using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankingContext _repo;

        public BankRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Bank> GetAll()
        {
            return _repo.Banks.AsNoTracking().ToList();
        }

        public Bank? GetById(int id)
        {
            return _repo.Banks.Find(id);
        }

        public Bank Add(Bank bank)
        {
            _repo.Banks.Add(bank);
            _repo.SaveChanges();
            return bank;
        }

        //public Bank Update(int id, Bank bank)
        //{
        //    var existing = _repo.Banks.Find(id);
        //    if (existing == null) return null!;

        //    existing.Name = bank.Name;
        //    existing.Address = bank.Address;
        //    existing.IFSCCODE = bank.IFSCCODE; // ✅ update IFSC too

        //    _repo.SaveChanges();
        //    return existing;
        //}


        //public bool Delete(int id)
        //{
        //    var bank = _repo.Banks.Find(id);
        //    if (bank == null) return false;

        //    _repo.Banks.Remove(bank);
        //    _repo.SaveChanges();
        //    return true;
        //}
    }
}
