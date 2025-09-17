
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repositories
{
    public class SalaryDisbursementRepository : ISalaryDisbursementRepository
    {
        private readonly BankingContext _ctx;

        public SalaryDisbursementRepository(BankingContext ctx) => _ctx = ctx;

        public List<SalaryDisbursement> GetAll()
        {
            return _ctx.SalaryDisbursements
                .Include(s => s.Employee)
                .ToList();
        }

        public SalaryDisbursement Add(SalaryDisbursement disbursement)
        {
            _ctx.SalaryDisbursements.Add(disbursement);
            _ctx.SaveChanges();
            return disbursement;
        }

        public SalaryDisbursement? Update(int id, SalaryDisbursement disbursement)
        {
            var existing = _ctx.SalaryDisbursements.Find(id);
            if (existing == null) return null;

            existing.Amount = disbursement.Amount;
            existing.Date = disbursement.Date;
            existing.BatchId = disbursement.BatchId;
            existing.EmployeeId = disbursement.EmployeeId;

            _ctx.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = _ctx.SalaryDisbursements.Find(id);
            if (existing == null) return false;

            _ctx.SalaryDisbursements.Remove(existing);
            _ctx.SaveChanges();
            return true;
        }

        public SalaryDisbursement? Find(int id)
        {
            return _ctx.SalaryDisbursements
                .Include(s => s.Employee)
                .FirstOrDefault(s => s.DisbursementId == id);
        }

        public List<SalaryDisbursement> GetByEmployee(int employeeId)
        {
            return _ctx.SalaryDisbursements
                .Where(s => s.EmployeeId == employeeId)
                .ToList();
        }
    }
}
