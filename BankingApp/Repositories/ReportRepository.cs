using System.Collections.Generic;
using System.Linq;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly BankingContext _repo;

        public ReportRepository(BankingContext context)
        {
            _repo = context;
        }

        public IEnumerable<Report> GetAll()
        {
            return _repo.Reports
                .Include(r => r.GeneratedBy)
                .ToList();
        }

        public Report? GetById(int id)
        {
            return _repo.Reports
                .Include(r => r.GeneratedBy)
                .FirstOrDefault(r => r.ReportId == id);
        }

        public Report Add(Report report)
        {
            _repo.Reports.Add(report);
            _repo.SaveChanges();
            return report;
        }

        public Report Update(int id, Report report)
        {
            var existing = _repo.Reports.Find(id);
            if (existing == null) return null!;

            _repo.Entry(existing).CurrentValues.SetValues(report);
            _repo.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var report = _repo.Reports.Find(id);
            if (report == null) return false;

            _repo.Reports.Remove(report);
            _repo.SaveChanges();
            return true;
        }
    }
}
