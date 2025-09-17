using BankingApp.Models;
using BankingApp.Repositories;

namespace BankingApp.Services
{
    public class SalaryDisbursementService : ISalaryDisbursementService
    {
        private readonly ISalaryDisbursementRepository _repository;

        public SalaryDisbursementService(ISalaryDisbursementRepository repository)
        {
            _repository = repository;
        }

        public List<SalaryDisbursement> GetAll()
        {
            return _repository.GetAll();
        }

        public SalaryDisbursement Add(SalaryDisbursement disbursement)
        {
            return _repository.Add(disbursement);
        }

        public SalaryDisbursement? Update(int id, SalaryDisbursement disbursement)
        {
            return _repository.Update(id, disbursement);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public SalaryDisbursement? Find(int id)
        {
            return _repository.Find(id);
        }

        public List<SalaryDisbursement> GetByEmployee(int employeeId)
        {
            return _repository.GetByEmployee(employeeId);
        }
    }
}
