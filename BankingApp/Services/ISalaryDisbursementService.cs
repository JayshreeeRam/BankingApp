using BankingApp.Models;

namespace BankingApp.Services
{
    public interface ISalaryDisbursementService
    {
        List<SalaryDisbursement> GetAll();
        SalaryDisbursement Add(SalaryDisbursement disbursement);
        SalaryDisbursement? Update(int id, SalaryDisbursement disbursement);
        bool Delete(int id);
        SalaryDisbursement? Find(int id);
        List<SalaryDisbursement> GetByEmployee(int employeeId);
    }
}
