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
        private readonly IBankRepository _bankRepo;

        public EmployeeService(IEmployeeRepository repo, IClientRepository clientRepo, IBankRepository bankRepo)
        {
            _repo = repo;
            _clientRepo = clientRepo;
            _bankRepo = bankRepo;
        }

        private EmployeeDto ToDto(Employee e)
        {
            var bank = e.EmployeeClient != null ? _bankRepo.GetById(e.EmployeeClient.BankId) : null;
            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeClientId = e.EmployeeClientId,
                EmployeeName = e.EmployeeClient?.Name ?? e.EmployeeClient?.User?.Username,
                BankId = e.EmployeeClient?.BankId ?? 0,
                BankName = bank != null ? bank.Name : "Unknown",
                SenderClientId = e.EmployerClientId,
                SenderName = e.EmployerClient?.Name ?? e.EmployerClient?.User?.Username,
                Salary = e.Salary,
                Department = e.Department
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

        public EmployeeDto Add(CreateEmployeeDto dto)
        {
            var employeeClient = _clientRepo.GetById(dto.EmployeeClientId);
            var senderClient = _clientRepo.GetById(dto.SenderClientId);

            if (employeeClient == null) throw new KeyNotFoundException("Employee client not found.");
            if (senderClient == null) throw new KeyNotFoundException("Sender client not found.");

            var employee = new Employee
            {
                EmployeeClientId = employeeClient.ClientId,
                EmployerClientId = senderClient.ClientId,
                Salary = dto.Salary,
                BankId = employeeClient.BankId,
                Name = employeeClient.Name ,
                Department = dto.Department
            };

            var added = _repo.Add(employee);
            return ToDto(added);
        }


        public EmployeeDto? Update(int id, UpdateEmployeeDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            var employeeClient = _clientRepo.GetById(dto.EmployeeClientId);
            var senderClient = _clientRepo.GetById(dto.SenderClientId);

            if (employeeClient == null) throw new KeyNotFoundException("Employee client not found.");
            if (senderClient == null) throw new KeyNotFoundException("Sender client not found.");

            existing.EmployeeClientId = employeeClient.ClientId;
            existing.EmployerClientId = senderClient.ClientId;
            existing.Salary = dto.Salary;
            existing.BankId = employeeClient.BankId;
            existing.Name = employeeClient.Name; 
            existing.Department = dto.Department;

            var updated = _repo.Update(id, existing);
            return updated == null ? null : ToDto(updated);
        }


        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
