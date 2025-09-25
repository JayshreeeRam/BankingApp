using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IBankService
    {
        IEnumerable<BankDto> GetAll();
        BankDto? GetById(int id);
        BankDto Add(CreateBankDto bankDto);
        //BankDto? Update(int id, BankDto bankDto);
        //bool Delete(int id);
    }
}
