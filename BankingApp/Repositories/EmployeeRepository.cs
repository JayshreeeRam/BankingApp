using System.Collections.Generic;
using System.Linq;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
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
                .Include(e => e.SalaryDisbursements) // Include salary info
                .ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .Include(e => e.SalaryDisbursements)
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
            var existing = _context.Employees.Find(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(employee);
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
