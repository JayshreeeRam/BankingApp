using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();
        EmployeeDto? GetById(int id);
        EmployeeDto Add(CreateEmployeeDto dto);
        EmployeeDto? Update(int id, UpdateEmployeeDto dto);
        bool Delete(int id);
    }
}
