
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Repository
{
    public class SalaryDisbursementRepository : ISalaryDisbursementRepository
    {
        private readonly BankingContext _repo;

        public SalaryDisbursementRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<SalaryDisbursement> GetAll()
        {
            return _repo.SalaryDisbursements
                        .Include(s => s.Employee)
                        .Where(s => s.EmployeeId != null) 
                        .ToList();
        }


        public SalaryDisbursement? GetById(int id)
        {
            return _repo.SalaryDisbursements
                           .Include(s => s.Employee)
                           .FirstOrDefault(s => s.DisbursementId == id);
        }

        public SalaryDisbursement Add(SalaryDisbursement salary)
        {
            _repo.SalaryDisbursements.Add(salary);
            _repo.SaveChanges();
            return salary;
        }

        public SalaryDisbursement Update(int id, SalaryDisbursement salary)
        {
            var existing = _repo.SalaryDisbursements.Find(id);
            if (existing == null) return null!;

            existing.EmployeeId = salary.EmployeeId;
            existing.Amount = salary.Amount;
            existing.Date = salary.Date;
            existing.Status = salary.Status;
            existing.BatchId = salary.BatchId;

            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var salary = _repo.SalaryDisbursements.Find(id);
            if (salary == null) return false;

            _repo.SalaryDisbursements.Remove(salary);
            _repo.SaveChanges();
            return true;
        }
    }
}
