using System.Collections.Generic;
using System.Linq;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{

    public class SalaryDisbursementService : ISalaryDisbursementService
    {
        private readonly ISalaryDisbursementRepository _repo;

        public SalaryDisbursementService(ISalaryDisbursementRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<SalaryDisbursementDto> GetAll()
        {
            return _repo.GetAll().Select(s => new SalaryDisbursementDto
            {
                EmployeeId = s.EmployeeId,
                Amount = s.Amount,
                Date = s.Date,
                Status = s.Status,
                BatchId = s.BatchId
            });
        }

        public SalaryDisbursementDto? GetById(int id)
        {
            var s = _repo.GetById(id);
            if (s == null) return null;

            return new SalaryDisbursementDto
            {
                EmployeeId = s.EmployeeId,
                Amount = s.Amount,
                Date = s.Date,
                Status = s.Status,
                BatchId = s.BatchId
            };
        }

        public SalaryDisbursement Add(SalaryDisbursementDto dto)
        {
            var salary = new SalaryDisbursement
            {
                EmployeeId = dto.EmployeeId,
                Amount = dto.Amount,
                Date = dto.Date,
                Status = dto.Status,
                BatchId = dto.BatchId
            };
            return _repo.Add(salary);
        }

        public SalaryDisbursement? Update(int id, SalaryDisbursementDto dto)
        {
            var salary = new SalaryDisbursement
            {
                EmployeeId = dto.EmployeeId,
                Amount = dto.Amount,
                Date = dto.Date,
                Status = dto.Status,
                BatchId = dto.BatchId
            };
            return _repo.Update(id, salary);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
