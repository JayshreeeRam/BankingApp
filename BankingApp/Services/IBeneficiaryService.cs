using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IBeneficiaryService
    {
        IEnumerable<Beneficiary> GetAll();
        Beneficiary GetById(int id);
        Beneficiary Add(Beneficiary Beneficiary);
        Beneficiary Update(Beneficiary Beneficiary);
        void Delete(int id);
    }
}
