using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface ISalaryDisbursementService
    {
        IEnumerable<SalaryDisbursementDto> GetAll();
        SalaryDisbursementDto? GetById(int id);
        SalaryDisbursement Add(SalaryDisbursementDto dto);
        SalaryDisbursement? Update(int id, SalaryDisbursementDto dto);
        bool Delete(int id);
    }
}
