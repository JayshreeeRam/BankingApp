
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly BankingContext _context;

        public BeneficiaryRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Beneficiary> GetAll()
        {
            return _context.Beneficiaries.AsNoTracking().ToList();
        }

        public Beneficiary? GetById(int id)
        {
            return _context.Beneficiaries.Find(id);
        }

        public Beneficiary Add(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            _context.SaveChanges();
            return beneficiary;
        }

        public Beneficiary Update(int id, Beneficiary beneficiary)
        {
            var existing = _context.Beneficiaries.Find(id);
            if (existing == null) return null!;

            existing.BankName = beneficiary.BankName;
            existing.AccountNo = beneficiary.AccountNo;
            existing.IFSCCode = beneficiary.IFSCCode;
            existing.ClientId = beneficiary.ClientId;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var beneficiary = _context.Beneficiaries.Find(id);
            if (beneficiary == null) return false;

            _context.Beneficiaries.Remove(beneficiary);
            _context.SaveChanges();
            return true;
        }
    }
}
