using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface ISalaryDisbursementRepository
    {
        IEnumerable<SalaryDisbursement> GetAll();
        SalaryDisbursement? GetById(int id);
        SalaryDisbursement Add(SalaryDisbursement salary);
        SalaryDisbursement Update(int id, SalaryDisbursement salary);
        bool Delete(int id);

    }
}