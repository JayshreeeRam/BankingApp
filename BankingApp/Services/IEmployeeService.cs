using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();
        EmployeeDto? GetById(int id);
        EmployeeDto Add(EmployeeDto employeeDto);
        EmployeeDto Update(int id, EmployeeDto employeeDto);
        bool Delete(int id);
    }
}
