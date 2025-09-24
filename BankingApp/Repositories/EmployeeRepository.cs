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
                .Include(e => e.Client)
                    .ThenInclude(c => c.User)  // fetch user for name
                .Include(e => e.Bank)
                .AsNoTracking()
                .ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .Include(e => e.Client)
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

            if (employee.ClientId != 0)
                existing.ClientId = employee.ClientId;

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
