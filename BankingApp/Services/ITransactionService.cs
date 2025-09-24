using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDto> GetAll();
        TransactionDto? GetById(int id);
        TransactionDto Add(TransactionDto dto);
        TransactionDto? Update(int id, TransactionDto dto);
        bool Delete(int id);
        IEnumerable<TransactionDto> GetByAccount(int accountId);
    }
}
