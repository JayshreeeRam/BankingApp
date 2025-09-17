using BankingApp.Models;
using BeneficiaryingApp.Repositories;

namespace BankingApp.Services
{
    public class BeneficiaryService:IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }
        public Beneficiary Add(Beneficiary beneficiary)
        {
            return _beneficiaryRepository.Add(beneficiary);
        }
        public void Delete(int id)
        {
            _beneficiaryRepository.Delete(id);
        }
        public IEnumerable<Beneficiary> GetAll()
        {
            return _beneficiaryRepository.GetAll();
        }
        public Beneficiary GetById(int id)
        {
            return _beneficiaryRepository.GetById(id);
        }
        public Beneficiary Update(Beneficiary beneficiary)
        {
            return _beneficiaryRepository.Update(beneficiary);
        }
    }
}
