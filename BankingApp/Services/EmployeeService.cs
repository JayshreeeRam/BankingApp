using System.Collections.Generic;
using System.Linq;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return _repo.GetAll().Select(e => MapToDto(e));
        }

        public EmployeeDto? GetById(int id)
        {
            var employee = _repo.GetById(id);
            return employee == null ? null : MapToDto(employee);
        }

        public EmployeeDto Add(EmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                BankId = dto.BankId,
                ClientId = dto.ClientId
            };
            var added = _repo.Add(employee);
            return MapToDto(added);
        }

        public EmployeeDto? Update(int id, EmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                BankId = dto.BankId,
                ClientId = dto.ClientId
            };
            var updated = _repo.Update(id, employee);
            return updated == null ? null : MapToDto(updated);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        private EmployeeDto MapToDto(Employee e)
        {
            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                BankId = e.BankId,
                ClientId = e.ClientId,
                SalaryDisbursements = e.SalaryDisbursements?.Select(s => new SalaryDisbursementDto
                {
                    
                    EmployeeId = s.EmployeeId,
                    Amount = s.Amount,
                    Status = s.Status, // ✅ Convert enum → string
                    Date = s.Date,
                    BatchId = s.BatchId
                }).ToList()
            };
        }

    }
}
