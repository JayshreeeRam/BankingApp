using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class BankService:IBankService
    {
        private readonly IBankRepository _bankRepository;
        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
        IEnumerable<Bank> IBankService.GetAll()
        {
            return _bankRepository.GetAll();
        }
        Bank IBankService.GetById(int id)
        {
            return _bankRepository.GetById(id);
        }
        Bank IBankService.Add(Bank bank)
        {
            return _bankRepository.Add(bank);
        }
        Bank IBankService.Update(Bank bank)
        {
            return _bankRepository.Update(bank);
        }
        void IBankService.Delete(int id)
        {
            _bankRepository.Delete(id);
        }

    }
}
