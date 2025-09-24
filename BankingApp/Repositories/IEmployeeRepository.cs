using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        Employee Add(Employee employee);
        Employee? Update(int id, Employee employee);
        bool Delete(int id);
    }
}
