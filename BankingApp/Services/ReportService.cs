using System.Collections.Generic;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repo;

        public ReportService(IReportRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Report> GetAll()
        {
            return _repo.GetAll();
        }

        public Report? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public Report Add(ReportDto dto)
        {
            var report = new Report
            {
                ReportType = dto.ReportType,
                FilePath = dto.FilePath,
                GeneratedByUserId = dto.GeneratedByUserId
            };
            return _repo.Add(report);
        }

        public Report? Update(int id, ReportDto dto)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return null;

            existing.ReportType = dto.ReportType;
            existing.FilePath = dto.FilePath;
            existing.GeneratedByUserId = dto.GeneratedByUserId;

            return _repo.Update(id, existing);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
