using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetAll();
        Report? GetById(int id);
        Report Add(Report report);
        Report Update(int id, Report report);
        bool Delete(int id);
    }
}
