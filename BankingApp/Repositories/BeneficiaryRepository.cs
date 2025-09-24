
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly BankingContext _repo;

        public BeneficiaryRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Beneficiary> GetAll()
        {
            return _repo.Beneficiaries
                        .Include(b => b.Client)
                            .ThenInclude(c => c.User)   // include User for username
                        .AsNoTracking()
                        .ToList();
        }

        public Beneficiary? GetById(int id)
        {
            return _repo.Beneficiaries
                        .Include(b => b.Client)
                            .ThenInclude(c => c.User)   // include User for username
                        .FirstOrDefault(b => b.BeneficiaryId == id);
        }


        public Beneficiary Add(Beneficiary beneficiary)
        {
            _repo.Beneficiaries.Add(beneficiary);
            _repo.SaveChanges();
            return beneficiary;
        }

        public Beneficiary Update(int id, Beneficiary beneficiary)
        {
            var existing = _repo.Beneficiaries.Find(id);
            if (existing == null) return null!;

            existing.BankName = beneficiary.BankName;
            existing.AccountNo = beneficiary.AccountNo;
            existing.IFSCCode = beneficiary.IFSCCode;
            existing.ClientId = beneficiary.ClientId;

            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var beneficiary = _repo.Beneficiaries.Find(id);
            if (beneficiary == null) return false;

            _repo.Beneficiaries.Remove(beneficiary);
            _repo.SaveChanges();
            return true;
        }
    }
}
