using BankingApp.Models;

namespace BankingApp.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Employee Add(Employee Employee);
        Employee Update(Employee Employee);
        void Delete(int id);
    }
}
