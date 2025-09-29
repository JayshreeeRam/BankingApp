using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;

        public ClientService(
            IClientRepository repo,
            IAccountService accountService,
            IUserRepository userRepository)
        {
            _repo = repo;
            _accountService = accountService;
            _userRepository = userRepository;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            return _repo.GetAll().Select(MapToDto).ToList();
        }

        public ClientDto? GetById(int id)
        {
            var client = _repo.GetById(id);
            if (client == null) return null;

            return MapToDto(client);
        }

        public ClientDto Add(CreateClientDto dto)
        {

            var user = _userRepository.GetById(dto.UserId);
            if (user == null) throw new Exception("User not found");

            var client = new Client
            {
                Name = user.Username,
                Address = dto.Address,
                BankId = dto.BankId,
                UserId = dto.UserId,
                AccountType = dto.AccountType,
                VerificationStatus = AccountStatus.Pending
            };

            var created = _repo.Add(client);
            return MapToDto(created);
        }

        public ClientDto? Update(int id, UpdateClientDto dto)
        {
            var client = _repo.GetById(id);
            if (client == null) return null;
            if (dto.Name != null) client.Name = dto.Name;
            if (dto.Address != null) client.Address = dto.Address;

            var updated = _repo.Update(id, client);
            return MapToDto(updated);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        // Approve client and generate account
        public ClientDto ApproveClient(int clientId)
        {
            var client = _repo.GetById(clientId);
            if (client == null) throw new Exception("Client not found");
            if (client.VerificationStatus != AccountStatus.Pending)
                throw new Exception("Client is not pending");

            // Approve client
            client.VerificationStatus = AccountStatus.Active;

            var accountDto = new CreateAccountDto
            {
                ClientId = client.ClientId,
                AccountType = client.AccountType,
                Balance = 0
            };

            var account = _accountService.AddAccount(accountDto);

            
            _repo.Update(client.ClientId, client);

            return MapToDto(client);
        }

        private ClientDto MapToDto(Client client)
        {
            return new ClientDto
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Address = client.Address,
                BankId = client.BankId,
                UserId = client.UserId,
                VerificationStatus = client.VerificationStatus,
                AccountType = client.AccountType,
                AccountNo = client.Account != null ? client.Account.AccountNumber : null
            };
        }
    }
}
