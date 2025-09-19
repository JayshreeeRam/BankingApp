using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IAccountService
    {
        IEnumerable<AccountDto> GetAll();
        AccountDto? GetById(int id);
        IEnumerable<AccountDto> GetByClientId(int clientId);
        AccountDto Add(CreateAccountDto dto);
        AccountDto? Update(int id, UpdateAccountDto dto);
        bool Delete(int id);
    }
}
