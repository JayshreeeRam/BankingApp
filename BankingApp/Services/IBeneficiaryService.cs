using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IBeneficiaryService
    {
        IEnumerable<BeneficiaryDto> GetAll();
        BeneficiaryDto? GetById(int id);
        BeneficiaryDto Add(int id);
        //BeneficiaryDto? Update(int id, BeneficiaryDto dto);
        bool Delete(int id);
    }
}
