using BankingApp.DTOs;
using BankingApp.Models;
using System.Collections.Generic;

namespace BankingApp.Services
{
    public interface ISalaryDisbursementService
    {
        IEnumerable<SalaryDisbursementDto> GetAll();
        SalaryDisbursementDto? GetById(int id);
        SalaryDisbursementDto Add(SalaryDisbursementDto dto);
        SalaryDisbursementDto? Update(int id, SalaryDisbursementDto dto);
        bool Delete(int id);

        SalaryDisbursementDto? ApproveSalary(int id);

        // Add this new method to the interface
       
        IEnumerable<SalaryDisbursementDto> ApproveSalaryByBatch(int batchId);
        
        IEnumerable<Transaction> GetTransactionHistoryForClient(int clientId);
        IEnumerable<Transaction> GetTransactionHistoryForEmployee(int employeeId);

    }
}
