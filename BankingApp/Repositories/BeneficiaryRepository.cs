using BankingApp.Models;
using BeneficiaryingApp.Repositories;

namespace BankingApp.Repositories
{
    public class BeneficiaryRepository:IBeneficiaryRepository
    {
        private readonly BankingContext _context;
        public BeneficiaryRepository(BankingContext context)
        {
            _context = context;
        }

        Beneficiary IBeneficiaryRepository.Add(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            _context.SaveChanges();
            return beneficiary;
        }
        void IBeneficiaryRepository.Delete(int id)
        {
            var beneficiary = _context.Beneficiaries.Find(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
                _context.SaveChanges();
            }
        }
        IEnumerable<Beneficiary> IBeneficiaryRepository.GetAll()
        {
            return _context.Beneficiaries.ToList();
        }
        Beneficiary IBeneficiaryRepository.GetById(int id)
        {
            return _context.Beneficiaries.Find(id);
        }
        Beneficiary IBeneficiaryRepository.Update(Beneficiary beneficiary)
        {
            var existingBeneficiary = _context.Beneficiaries.Find(beneficiary.BeneficiaryId);
            if (existingBeneficiary != null)
            {
                existingBeneficiary.BankName = beneficiary.BankName;
                existingBeneficiary.AccountNo = beneficiary.AccountNo;
                existingBeneficiary.BankName = beneficiary.BankName;
                existingBeneficiary.IFSCCode = beneficiary.IFSCCode;
                existingBeneficiary.ClientId = beneficiary.ClientId;
                _context.SaveChanges();
            }
            return existingBeneficiary;
        }
    }
}
