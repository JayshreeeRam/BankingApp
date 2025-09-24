using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IBankRepository
    {
        IEnumerable<Bank> GetAll();
        Bank? GetById(int id);
        Bank Add(Bank bank);
        Bank Update(int id, Bank bank);
        bool Delete(int id);
    }
}
