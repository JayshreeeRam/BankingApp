using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface ITransactionService
    {
        TransactionDto Register(RegisterTransactionDto dto);
        TransactionDto? UpdateTransaction(int id, UpdateTransactionDto dto);
        bool SoftDeleteTransaction(int id);
        TransactionDto? GetById(int id);
        IEnumerable<TransactionDto> GetByAccountId(int accountId);
    }
}
