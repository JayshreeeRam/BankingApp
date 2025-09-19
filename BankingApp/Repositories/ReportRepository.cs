using System.Collections.Generic;
using System.Linq;
using BankingApp.Models;
using BankingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly BankingContext _context;

        public ReportRepository(BankingContext context)
        {
            _context = context;
        }

        public IEnumerable<Report> GetAll()
        {
            return _context.Reports
                .Include(r => r.GeneratedBy)
                .ToList();
        }

        public Report? GetById(int id)
        {
            return _context.Reports
                .Include(r => r.GeneratedBy)
                .FirstOrDefault(r => r.ReportId == id);
        }

        public Report Add(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();
            return report;
        }

        public Report Update(int id, Report report)
        {
            var existing = _context.Reports.Find(id);
            if (existing == null) return null!;

            _context.Entry(existing).CurrentValues.SetValues(report);
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null) return false;

            _context.Reports.Remove(report);
            _context.SaveChanges();
            return true;
        }
    }
}
