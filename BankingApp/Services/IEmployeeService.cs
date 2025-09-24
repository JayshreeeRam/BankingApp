using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();
        EmployeeDto? GetById(int id);
        EmployeeDto Add(EmployeeDto dto);
        EmployeeDto? Update(int id, EmployeeDto dto);
        bool Delete(int id);
    }
}
