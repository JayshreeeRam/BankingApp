using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repo;

        public BeneficiaryService(IBeneficiaryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<BeneficiaryDto> GetAll()
        {
            return _repo.GetAll().Select(b => new BeneficiaryDto
            {
                BeneficiaryId = b.BeneficiaryId,
                BankName = b.BankName,
                AccountNo = b.AccountNo,
                IFSCCode = b.IFSCCode!,
                ClientId = b.ClientId
            });
        }

        public BeneficiaryDto? GetById(int id)
        {
            var b = _repo.GetById(id);
            if (b == null) return null;

            return new BeneficiaryDto
            {
                BeneficiaryId = b.BeneficiaryId,
                BankName = b.BankName,
                AccountNo = b.AccountNo,
                IFSCCode = b.IFSCCode!,
                ClientId = b.ClientId
            };
        }

        public BeneficiaryDto Add(BeneficiaryDto dto)
        {
            var beneficiary = new Beneficiary
            {
                BankName = dto.BankName,
                AccountNo = dto.AccountNo,
                IFSCCode = dto.IFSCCode,
                ClientId = dto.ClientId
            };

            var created = _repo.Add(beneficiary);

            return new BeneficiaryDto
            {
                BeneficiaryId = created.BeneficiaryId,
                BankName = created.BankName,
                AccountNo = created.AccountNo,
                IFSCCode = created.IFSCCode!,
                ClientId = created.ClientId
            };
        }

        public BeneficiaryDto? Update(int id, BeneficiaryDto dto)
        {
            var beneficiary = new Beneficiary
            {
                BankName = dto.BankName,
                AccountNo = dto.AccountNo,
                IFSCCode = dto.IFSCCode,
                ClientId = dto.ClientId
            };

            var updated = _repo.Update(id, beneficiary);
            if (updated == null) return null;

            return new BeneficiaryDto
            {
                BeneficiaryId = updated.BeneficiaryId,
                BankName = updated.BankName,
                AccountNo = updated.AccountNo,
                IFSCCode = updated.IFSCCode!,
                ClientId = updated.ClientId
            };
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
