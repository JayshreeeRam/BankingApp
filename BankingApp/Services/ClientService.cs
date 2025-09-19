using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;

        public ClientService(IClientRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            return _repo.GetAll().Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                Name = c.Name,
                Address = c.Address,
                BankId = c.BankId,
                UserId = c.UserId,
                AccountNo = c.AccountNo,
                VerificationStatus = c.VerificationStatus,
                AccountType = c.AccountType
            });
        }

        public ClientDto? GetById(int id)
        {
            var client = _repo.GetById(id);
            if (client == null) return null;

            return new ClientDto
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Address = client.Address,
                BankId = client.BankId,
                UserId = client.UserId,
                AccountNo = client.AccountNo,
                VerificationStatus = client.VerificationStatus,
                AccountType = client.AccountType
            };
        }

        public ClientDto Add(CreateClientDto dto)
        {
            var client = new Client
            {
                Name = dto.Name,
                Address = dto.Address,
                BankId = (int)dto.BankId,
                UserId = (int)dto.UserId,
                AccountNo = dto.AccountNo,
                VerificationStatus = AccountStatus.Pending, // always default to Pending
                AccountType = dto.AccountType.HasValue ? dto.AccountType.Value : default // updated to handle nullable
            };

            var created = _repo.Add(client);

            return new ClientDto
            {
                ClientId = created.ClientId,
                Name = created.Name,
                Address = created.Address,
                BankId = created.BankId,
                UserId = created.UserId,
                AccountNo = created.AccountNo,
                VerificationStatus = created.VerificationStatus,
                AccountType = created.AccountType
            };
        }

        public ClientDto? Update(int id, UpdateClientDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            if (dto.Name != null) existing.Name = dto.Name;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.BankId.HasValue) existing.BankId = (int)dto.BankId;
            if (dto.UserId.HasValue) existing.UserId = (int)dto.UserId;
            if (dto.AccountNo != null) existing.AccountNo = dto.AccountNo;
            if (dto.VerificationStatus != null)
                existing.VerificationStatus = (dto.VerificationStatus); // corrected missing argument
            if (dto.AccountType != null)
                existing.AccountType = (dto.AccountType);

            var updated = _repo.Update(id, existing);

            return new ClientDto
            {
                ClientId = updated.ClientId,
                Name = updated.Name,
                Address = updated.Address,
                BankId = updated.BankId,
                UserId = updated.UserId,
                AccountNo = updated.AccountNo,
                VerificationStatus = updated.VerificationStatus,
                AccountType = updated.AccountType
            };
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
