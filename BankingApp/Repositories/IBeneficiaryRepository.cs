using BankingApp.Models;


namespace BeneficiaryingApp.Repositories
{
    public interface IBeneficiaryRepository
    {
        IEnumerable<Beneficiary> GetAll();
        Beneficiary GetById(int id);
        Beneficiary Add(Beneficiary Beneficiary);
        Beneficiary Update(Beneficiary Beneficiary);
        void Delete(int id);
    }
}
