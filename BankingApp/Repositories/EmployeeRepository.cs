using BankingApp.Models;

namespace BankingApp.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly BankingContext _context;

        public EmployeeRepository(BankingContext context)
        {
            _context = context;
        }
        Employee IEmployeeRepository.Add(Employee Employee)
        {
            if (Employee.BankId.HasValue && !_context.Banks.Any(b => b.BankId == Employee.BankId))
            {
                throw new InvalidOperationException("The specified BankId does not exist.");
            }
            _context.Employees.Add(Employee);
            _context.SaveChanges();
            return Employee;
        }
        void IEmployeeRepository.Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
        IEnumerable<Employee> IEmployeeRepository.GetAll()
        {
            return _context.Employees.ToList();
        }
        Employee IEmployeeRepository.GetById(int id)
        {
            return _context.Employees.Find(id);
        }
        Employee IEmployeeRepository.Update(Employee Employee)
        {
            var existingEmployee = _context.Employees.Find(Employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = Employee.Name;
                 _context.SaveChanges();
            }
            return existingEmployee;
        }
    }
}
