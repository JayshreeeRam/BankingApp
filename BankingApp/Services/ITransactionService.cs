using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDto> GetAll();
        TransactionDto? GetById(int id);
        Transaction Add(TransactionDto dto);
        Transaction? Update(int id, TransactionDto dto);
        bool Delete(int id);
        IEnumerable<TransactionDto> GetByAccount(int accountId);
    }

}
