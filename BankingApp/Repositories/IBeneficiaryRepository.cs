using BankingApp.Models;

namespace BankingApp.Repository
{
    public interface IBeneficiaryRepository
    {
        IEnumerable<Beneficiary> GetAll();
        Beneficiary? GetById(int id);
        Beneficiary Add(Beneficiary beneficiary);
        Beneficiary Update(int id, Beneficiary beneficiary);
        bool Delete(int id);
    }
}
