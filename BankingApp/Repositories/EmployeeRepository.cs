using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BankingContext _context;

        public EmployeeRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees
                .Include(e => e.EmployerClient)
                    .ThenInclude(c => c.User) // Employer’s User (company account)
                .Include(e => e.EmployeeClient)
                    .ThenInclude(c => c.User) // Employee’s User (personal account)
                .Include(e => e.Bank)
                .AsNoTracking()
                .ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .Include(e => e.EmployerClient)
                    .ThenInclude(c => c.User)
                .Include(e => e.EmployeeClient)
                    .ThenInclude(c => c.User)
                .Include(e => e.Bank)
                .AsNoTracking()
                .FirstOrDefault(e => e.EmployeeId == id);
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee? Update(int id, Employee employee)
        {
            var existing = _context.Employees
                                   .FirstOrDefault(e => e.EmployeeId == id);

            if (existing == null) return null;

            // Update fields safely
            if (!string.IsNullOrWhiteSpace(employee.Name))
                existing.Name = employee.Name;

            if (employee.Salary > 0)
                existing.Salary = employee.Salary;

            if (employee.BankId != 0)
                existing.BankId = employee.BankId;

            if (employee.EmployerClientId != 0)
                existing.EmployerClientId = employee.EmployerClientId;

            if (employee.EmployeeClientId != 0)
                existing.EmployeeClientId = employee.EmployeeClientId;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }
    }
}
