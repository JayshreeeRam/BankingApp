using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        IEnumerable<Employee> IEmployeeService.GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }
        Employee IEmployeeService.GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }
        Employee IEmployeeService.CreateEmployee(Employee employee)
        {
            return _employeeRepository.Add(employee);
        }
        Employee IEmployeeService.UpdateEmployee(int id ,Employee employee)
        {
            return _employeeRepository.Update(employee);
        }
        bool IEmployeeService.DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
            return false;
        }
    }
}
