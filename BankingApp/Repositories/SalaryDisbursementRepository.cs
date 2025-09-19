
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Repository
{
    public class SalaryDisbursementRepository : ISalaryDisbursementRepository
    {
        private readonly BankingContext _context;

        public SalaryDisbursementRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<SalaryDisbursement> GetAll()
        {
            return _context.SalaryDisbursements
                           .Include(s => s.Employee)
                           .ToList();
        }

        public SalaryDisbursement? GetById(int id)
        {
            return _context.SalaryDisbursements
                           .Include(s => s.Employee)
                           .FirstOrDefault(s => s.DisbursementId == id);
        }

        public SalaryDisbursement Add(SalaryDisbursement salary)
        {
            _context.SalaryDisbursements.Add(salary);
            _context.SaveChanges();
            return salary;
        }

        public SalaryDisbursement Update(int id, SalaryDisbursement salary)
        {
            var existing = _context.SalaryDisbursements.Find(id);
            if (existing == null) return null!;

            existing.EmployeeId = salary.EmployeeId;
            existing.Amount = salary.Amount;
            existing.Date = salary.Date;
            existing.Status = salary.Status;
            existing.BatchId = salary.BatchId;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var salary = _context.SalaryDisbursements.Find(id);
            if (salary == null) return false;

            _context.SalaryDisbursements.Remove(salary);
            _context.SaveChanges();
            return true;
        }
    }
}
