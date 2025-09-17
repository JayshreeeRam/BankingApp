using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IBankService
    {
        IEnumerable<Bank> GetAll();
        Bank GetById(int id);
        Bank Add(Bank bank);
        Bank Update(Bank bank);
        void Delete(int id);
    }
}
