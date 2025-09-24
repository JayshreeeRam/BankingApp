using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;


        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
          
        }


        public IEnumerable<AccountDto> GetAll()
        {
            return _repo.GetAll().Select(MapToDto).ToList();
        }

        public AccountDto? GetById(int id)
        {
            var account = _repo.GetById(id);
            return account == null ? null : MapToDto(account);
        }

        public AccountDto? GetByClientId(int clientId)
        {
            var account = _repo.GetByClientId(clientId);
            if (account == null) return null;
            return MapToDto(account);
        }


        public AccountDto Add(CreateAccountDto dto)
        {
            var accountNumber = GenerateAccountNumber();
            var account = new Account(accountNumber)
            {

                AccountType = dto.AccountType.HasValue ? dto.AccountType.Value : default, // updated to handle nullable
                AccountStatus = AccountStatus.Active,
                Balance = dto.Balance,
                ClientId = dto.ClientId
            };

            var created = _repo.Add(account);
            return MapToDto(created);
        }

        public AccountDto AddAccount(CreateAccountDto dto)
        {
            // Generate a unique account number
            string accountNumber = GenerateAccountNumber();

            var account = new Account(accountNumber)
            {
                ClientId = dto.ClientId,
                AccountType =  (AccountType)dto.AccountType,
                AccountStatus = AccountStatus.Active,
                Balance = dto.Balance
            };

            var created = _repo.Add(account);
            return MapToDto(created);
        }

        // Service
        public AccountDto? Update(int id, UpdateAccountDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            if (dto.AccountType.HasValue)
                existing.AccountType = dto.AccountType.Value;

            if (dto.AccountStatus.HasValue)
                existing.AccountStatus = dto.AccountStatus.Value;

            if (dto.Balance.HasValue)
                existing.Balance = dto.Balance.Value;

            var updated = _repo.Update(id, existing);
            return updated == null ? null : MapToDto(updated);
        }

    public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        private AccountDto MapToDto(Account account)
        {
            return new AccountDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                AccountStatus = account.AccountStatus,
                Balance = account.Balance,
                ClientId = account.ClientId
            };
        }

        private string GenerateAccountNumber()
        {
            string accountNumber;
            var random = new Random();

            do
            {
                accountNumber = random.Next(0, 999999999).ToString("D9"); // 9 digits
                accountNumber = "1" + accountNumber + random.Next(0, 9).ToString(); // 11 digits
            }
            while (_repo.AccountNumberExists(accountNumber));

            return accountNumber;
        }


    }
}
