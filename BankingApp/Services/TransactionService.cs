using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public List<Transaction> GetAll()
        {
            return _repository.GetAll();
        }

        public Transaction Add(Transaction transaction)
        {
            return _repository.Add(transaction);
        }

        public Transaction? Update(int id, Transaction transaction)
        {
            return _repository.Update(id, transaction);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public Transaction? Find(int id)
        {
            return _repository.Find(id);
        }

        public List<Transaction> GetByAccount(int accountId)
        {
            return _repository.GetByAccount(accountId);
        }
    }
}
