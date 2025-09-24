using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IReportService
    {
        IEnumerable<Report> GetAll();
        Report? GetById(int id);
        Report Add(ReportDto dto);
        Report? Update(int id, ReportDto dto);
        bool Delete(int id);
    }
}
