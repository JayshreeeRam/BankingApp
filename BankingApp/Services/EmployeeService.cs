using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IClientRepository _clientRepo;

        public EmployeeService(IEmployeeRepository repo, IClientRepository clientRepo)
        {
            _repo = repo;
            _clientRepo = clientRepo;
        }

        // Convert Employee entity → DTO
        private EmployeeDto ToDto(Employee e)
        {
            // Fetch latest Client and Bank names
            var clientName = e.Client?.User?.Username ;
            var bankName = e.Bank?.Name;

            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                BankId = e.BankId,
                BankName = bankName,
                ClientId = e.ClientId,
                ClientName = clientName,
                Salary = e.Salary
            };
        }

        // Convert DTO → Employee entity (for Add/Update)
        private Employee ToEntity(EmployeeDto dto)
        {
            return new Employee
            {
                Name = dto.Name,        // Employee's own name
                BankId = dto.BankId,    // Only store the ID
                ClientId = dto.ClientId,// Only store the ID
                Salary = dto.Salary
            };
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return _repo.GetAll().Select(ToDto);
        }

        public EmployeeDto? GetById(int id)
        {
            var emp = _repo.GetById(id);
            return emp == null ? null : ToDto(emp);
        }

        public EmployeeDto Add(EmployeeDto dto)
        {
            var entity = ToEntity(dto);
            var emp = _repo.Add(entity);
            return ToDto(emp);
        }

        public EmployeeDto? Update(int id, EmployeeDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.BankId = dto.BankId;
            existing.ClientId = dto.ClientId;
            existing.Salary = dto.Salary;

            var updated = _repo.Update(id, existing);
            return updated == null ? null : ToDto(updated);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
