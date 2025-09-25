using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repo;

        public BankService(IBankRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<BankDto> GetAll()
        {
            return _repo.GetAll().Select(b => new BankDto
            {
                BankId = b.BankId,
                Name = b.Name,
                Address = b.Address,
                IFSCCODE = b.IFSCCODE 
            });
        }

        public BankDto? GetById(int id)
        {
            var bank = _repo.GetById(id);
            if (bank == null) return null;

            return new BankDto
            {
                BankId = bank.BankId,
                Name = bank.Name,
                Address = bank.Address,
                IFSCCODE = bank.IFSCCODE 
            };
        }

        public BankDto Add(CreateBankDto bankDto)
        {
            // Generate IFSC code
            var prefix = bankDto.Name.Length >= 3
                ? bankDto.Name.Substring(0, 3).ToUpper()
                : bankDto.Name.ToUpper();
            var random = new Random().Next(1000, 9999);
            var ifsc = $"{prefix}{random}X";

            var bank = new Bank
            {
                Name = bankDto.Name,
                Address = bankDto.Address,
                IFSCCODE = ifsc
            };

            var created = _repo.Add(bank);

            return new BankDto
            {
                BankId = created.BankId,
                Name = created.Name,
                Address = created.Address,
                IFSCCODE = created.IFSCCODE
            };
        }


        //public BankDto? Update(int id, BankDto bankDto)
        //{
        //    var bank = new Bank
        //    {
        //        Name = bankDto.Name,
        //        Address = bankDto.Address,
        //        IFSCCODE = bankDto.IFSCCODE 
        //    };

        //    var updated = _repo.Update(id, bank);
        //    if (updated == null) return null;

        //    return new BankDto
        //    {
        //        BankId = updated.BankId,
        //        Name = updated.Name,
        //        Address = updated.Address,
        //        IFSCCODE = updated.IFSCCODE
        //    };
        //}


        //public bool Delete(int id)
        //{
        //    return _repo.Delete(id);
        //}
    }
}
