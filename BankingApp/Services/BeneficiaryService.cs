using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IBankRepository _bankRepo;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepo, IClientRepository clientRepo, IBankRepository bankRepo)
        {
            _beneficiaryRepo = beneficiaryRepo;
            _clientRepo = clientRepo;
            _bankRepo = bankRepo;
        }

        public IEnumerable<BeneficiaryDto> GetAll()
        {
            return _beneficiaryRepo.GetAll().Select(MapToDto);
        }

        public BeneficiaryDto? GetById(int id)
        {
            var beneficiary = _beneficiaryRepo.GetById(id);
            return beneficiary == null ? null : MapToDto(beneficiary);
        }

        public BeneficiaryDto Add(int clientId)
        {
            
            var client = _clientRepo.GetById(clientId);
            var BankName = _bankRepo.GetById(client.BankId);
            Console.WriteLine("Bank Name is " + BankName.Name);
            if (client == null)
                throw new Exception("Client not found");

            var beneficiary = new Beneficiary
            {
                ClientId = client.ClientId,
                BankName = BankName.Name,
                AccountNo = client.Account?.AccountNumber,
                IFSCCode = BankName.IFSCCODE,

            };

            var created = _beneficiaryRepo.Add(beneficiary);

            return MapToDto(created);
        }

        // Update beneficiary
        //public BeneficiaryDto? Update(int id, BeneficiaryDto dto)
        //{

        //    var client = _clientRepo.GetById(dto.ClientId);
        //    var BankName = _bankRepo.GetById(client.BankId);
        //    if (client == null)
        //        throw new Exception("Client not found");

        //    var beneficiary = new Beneficiary
        //    {
        //        BeneficiaryId = id,
        //        ClientId = client.ClientId,
        //        BankName = BankName.Name,
        //        AccountNo = client.Account?.AccountNumber,
        //        IFSCCode = BankName.IFSCCODE,

        //    };

        //    var updated = _beneficiaryRepo.Update(id, beneficiary);
        //    return updated == null ? null : MapToDto(updated);
        //}

        // Delete beneficiary
        public bool Delete(int id)
        {
            return _beneficiaryRepo.Delete(id);
        }

        private BeneficiaryDto MapToDto(Beneficiary b)
        {
            return new BeneficiaryDto
            {
                BeneficiaryId = b.BeneficiaryId,
                ClientId = b.ClientId,
                ClientName = b.Client?.User.Username,
                BankName = b.BankName,
                AccountNo = b.AccountNo,
                IFSCCode = b.IFSCCode

            };
        }
    }
}
