using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public ClientService(
            IClientRepository repo,
            IAccountService accountService,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _repo = repo;
            _accountService = accountService;
            _userRepository = userRepository;
            _emailService = emailService;
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
        public ClientDto RejectClient(int id, string remark)
        {
            try
            {
                // Get the client using repository
                var client = _repo.GetById(id);
                if (client == null)
                    throw new ArgumentException($"Client with id {id} not found.");

                // Check if client is in pending status - using VerificationStatus instead of Status
                if (client.VerificationStatus != AccountStatus.Pending)
                    throw new InvalidOperationException($"Cannot reject client with status {client.VerificationStatus}. Only pending clients can be rejected.");

                // Update client status and rejection remark
                client.VerificationStatus = AccountStatus.Frozen;
                //(string clientEmail, string clientName, string remark)

                _emailService.SendRejectionEmail(client.User?.Email, client.Name, remark);

                // Update the client using repository
                var updatedClient = _repo.Update(id, client);

                // Map to DTO and return
                return MapToDto(updatedClient);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in RejectClient: {ex.Message}");
                throw;
            }
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
                AccountNo = client.Account != null ? client.Account.AccountNumber : null,
                //RejectionRemark = client.,
            };
        }
    }
}
