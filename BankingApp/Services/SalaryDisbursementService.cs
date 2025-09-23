using System;
using System.Collections.Generic;
using System.Linq;
using BankingApp.DTOs;
using BankingApp.Enums;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;

namespace BankingApp.Services
{
    public class SalaryDisbursementService : ISalaryDisbursementService
    {
        private readonly ISalaryDisbursementRepository _repo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;

        public SalaryDisbursementService(
            ISalaryDisbursementRepository repo,
            IEmployeeRepository employeeRepo,
            IAccountRepository accountRepo,
            ITransactionRepository transactionRepo)
        {
            _repo = repo;
            _employeeRepo = employeeRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }

        public IEnumerable<SalaryDisbursementDto> GetAll()
        {
            var allSalaries = _repo.GetAll();
            if (allSalaries == null || !allSalaries.Any())
                return new List<SalaryDisbursementDto>();

            return allSalaries.Select(s =>
            {
                var emp = _employeeRepo.GetById(s.EmployeeId);
                return new SalaryDisbursementDto
                {
                    DisbursementId = s.DisbursementId,
                    EmployeeId = s.EmployeeId,
                    ClientId = s.ClientId,
                    EmployeeName = emp?.Name,
                    SenderName = emp?.Client?.User?.Username,
                    Amount = s.Amount,
                    Date = s.Date,
                    Status = s.Status,
                    BatchId = s.BatchId
                };
            }).ToList();
        }

        public SalaryDisbursementDto? GetById(int id)
        {
            var s = _repo.GetById(id);
            if (s == null) return null;

            var emp = _employeeRepo.GetById(s.EmployeeId);
            return new SalaryDisbursementDto
            {
                DisbursementId = s.DisbursementId,
                EmployeeId = s.EmployeeId,
                ClientId = s.ClientId,
                EmployeeName = emp?.Name,
                SenderName = emp?.Client?.User?.Username ,
                Amount = s.Amount,
                Date = s.Date,
                Status = s.Status,
                BatchId = s.BatchId
            };
        }

        public SalaryDisbursementDto Add(SalaryDisbursementDto dto)
        {
            var emp = _employeeRepo.GetById(dto.EmployeeId);
            if (emp == null) throw new KeyNotFoundException("Employee not found.");

            var salary = new SalaryDisbursement
            {
                EmployeeId = emp.EmployeeId,
                ClientId = dto.ClientId,
                Amount = (decimal)emp.Salary,
                Date = DateTime.UtcNow,
                Status = PaymentStatus.Pending,
                BatchId = dto.BatchId
            };

            var added = _repo.Add(salary);

            return new SalaryDisbursementDto
            {
                DisbursementId = added.DisbursementId,
                EmployeeId = added.EmployeeId,
                ClientId = added.ClientId,
                EmployeeName = emp.Name,
                SenderName = emp.Client?.User?.Username,
                Amount = added.Amount,
                Date = added.Date,
                Status = added.Status,
                BatchId = added.BatchId
            };
        }

        public SalaryDisbursementDto? Update(int id, SalaryDisbursementDto dto)
        {
            var salary = _repo.GetById(id);
            if (salary == null) return null;

            salary.BatchId = dto.BatchId;
            salary.ClientId = dto.ClientId;
            _repo.Update(id, salary);

            var emp = _employeeRepo.GetById(salary.EmployeeId);
            return new SalaryDisbursementDto
            {
                DisbursementId = salary.DisbursementId,
                EmployeeId = salary.EmployeeId,
                ClientId = salary.ClientId,
                EmployeeName = emp?.Name,
                SenderName = emp?.Client?.User?.Username ,
                Amount = salary.Amount,
                Date = salary.Date,
                Status = salary.Status,
                BatchId = salary.BatchId
            };
        }

        public bool Delete(int id) => _repo.Delete(id);

        public SalaryDisbursementDto? ApproveSalary(int id)
        {
            var salary = _repo.GetById(id);
            if (salary == null) return null;

            var emp = _employeeRepo.GetById(salary.EmployeeId);
            if (emp == null) return null;

            var clientAccount = _accountRepo.GetByClientId(salary.ClientId);
            if (clientAccount == null || clientAccount.Balance < salary.Amount) return null;

            clientAccount.Balance -= salary.Amount;
            _accountRepo.Update(clientAccount.AccountId, clientAccount);

            salary.Status = PaymentStatus.Approved;
            _repo.Update(salary.DisbursementId, salary);

            var transaction = new Transaction
            {
                AccountId = clientAccount.AccountId,
                SenderId = salary.ClientId,
                ReceiverId = emp.EmployeeId,
                Amount = salary.Amount,
             TransactionDate = DateTime.UtcNow.ToLocalTime(),
                TransactionType = TransactionType.Credit,
                TransactionStatus = TransactionStatus.Success,
                SenderName = clientAccount.Client?.User?.Username,
                ReceiverName = emp.Name
            };
            _transactionRepo.Add(transaction);

            return new SalaryDisbursementDto
            {
                DisbursementId = salary.DisbursementId,
                EmployeeId = salary.EmployeeId,
                ClientId = salary.ClientId,
                EmployeeName = emp.Name,
                SenderName = clientAccount.Client?.User?.Username,
                Amount = salary.Amount,
                Date = salary.Date,
                Status = salary.Status,
                BatchId = salary.BatchId
            };
        }


        public IEnumerable<SalaryDisbursementDto> ApproveSalaryByBatch(int batchId)
        {
            var pendingSalaries = _repo.GetAll()
                .Where(s => s.BatchId == batchId && s.Status == PaymentStatus.Pending)
                .ToList();

            var approved = new List<SalaryDisbursementDto>();

            foreach (var salary in pendingSalaries)
            {
                var emp = _employeeRepo.GetById(salary.EmployeeId);
                var clientAccount = _accountRepo.GetByClientId(salary.ClientId);

                if (emp == null || clientAccount == null || clientAccount.Balance < salary.Amount)
                    continue; // skip if not enough balance or invalid

                // Deduct amount
                clientAccount.Balance -= salary.Amount;
                _accountRepo.Update(clientAccount.AccountId, clientAccount);

                // Approve salary
                salary.Status = PaymentStatus.Approved;
                _repo.Update(salary.DisbursementId, salary);

                // Create transaction
                var transaction = new Transaction
                {
                    AccountId = clientAccount.AccountId,
                    SenderId = salary.ClientId,
                    ReceiverId = emp.EmployeeId,
                    Amount = salary.Amount,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = TransactionType.Credit,
                    TransactionStatus = TransactionStatus.Success,
                    SenderName = clientAccount.Client?.User?.Username,
                    ReceiverName = emp.Name
                };
                _transactionRepo.Add(transaction);

                approved.Add(new SalaryDisbursementDto
                {
                    DisbursementId = salary.DisbursementId,
                    EmployeeId = emp.EmployeeId,
                    ClientId = salary.ClientId,
                    EmployeeName = emp.Name,
                    SenderName = clientAccount.Client?.User?.Username,
                    Amount = salary.Amount,
                    Date = salary.Date,
                    Status = salary.Status,
                    BatchId = salary.BatchId
                });
            }

            return approved;
        }

        public IEnumerable<Transaction> GetTransactionHistoryForClient(int clientId)
        {
            return _transactionRepo.GetAll()
                .Where(t => t.SenderId == clientId || t.ReceiverId == clientId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        public IEnumerable<Transaction> GetTransactionHistoryForEmployee(int employeeId)
        {
            return _transactionRepo.GetAll()
                .Where(t => t.ReceiverId == employeeId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }
    }
}
