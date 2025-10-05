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
                    EmployeeName = emp?.EmployeeClient?.User?.Username,
                    SenderName = emp?.EmployerClient?.User?.Username,
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
                EmployeeName = emp?.EmployeeClient?.User?.Username,
                SenderName = emp?.EmployerClient?.User?.Username,
                Amount = s.Amount,
                Date = s.Date,
                Status = s.Status,
                BatchId = s.BatchId
            };
        }

        public SalaryDisbursementDto Add(CreateSalaryDisbursementDto dto)
        {
            var emp = _employeeRepo.GetById(dto.EmployeeId);
            if (emp == null) throw new KeyNotFoundException("Employee not found.");

            if (emp.EmployeeClient == null)
                throw new Exception("Employee is not linked to any client.");

            var salary = new SalaryDisbursement
            {
                EmployeeId = emp.EmployeeId,
                ClientId = emp.EmployerClientId,            
                Amount = (decimal)emp.Salary,       
                Date = DateTime.UtcNow,
                Status = PaymentStatus.Pending,
                BatchId = emp.Department               
            };

            var added = _repo.Add(salary);

            return new SalaryDisbursementDto
            {
                DisbursementId = added.DisbursementId,
                EmployeeId = added.EmployeeId,
                ClientId = added.ClientId,
                EmployeeName = emp?.EmployeeClient?.User?.Username,
                SenderName = emp?.EmployerClient?.User?.Username,
                Amount = added.Amount,
                Date = added.Date,
                Status = added.Status,
                BatchId = emp.Department
            };
        }



        public SalaryDisbursementDto? Update(int id, SalaryDisbursementDto dto)
        {
            var salary = _repo.GetById(id);
            if (salary == null) return null;

            var emp = _employeeRepo.GetById(salary.EmployeeId);
            salary.BatchId = emp.Department;
            salary.ClientId = dto.ClientId;
            _repo.Update(id, salary);

            return new SalaryDisbursementDto
            {
                DisbursementId = salary.DisbursementId,
                EmployeeId = salary.EmployeeId,
                ClientId = salary.ClientId,
                EmployeeName = emp?.EmployeeClient?.User?.Username,
                SenderName = emp?.EmployerClient?.User?.Username,
                Amount = salary.Amount,
                Date = salary.Date,
                Status = salary.Status,
                BatchId = emp.Department
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

            // Correct ReceiverId here
            var transaction = new Transaction
            {
                AccountId = clientAccount.AccountId,
                SenderId = salary.ClientId,               
                ReceiverId = emp.EmployeeClientId,       
                Amount = salary.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Credit,
                TransactionStatus = TransactionStatus.Success,
                ReceiverName = emp?.EmployeeClient?.User?.Username,
                SenderName = emp?.EmployerClient?.User?.Username,
            };
            _transactionRepo.Add(transaction);

            return new SalaryDisbursementDto
            {
                DisbursementId = salary.DisbursementId,
                EmployeeId = salary.EmployeeId,
                ClientId = salary.ClientId,
                EmployeeName = emp?.EmployeeClient?.User?.Username,
                SenderName = emp?.EmployerClient?.User?.Username,
                Amount = salary.Amount,
                Date = salary.Date,
                Status = salary.Status,
                BatchId = emp.Department
            };
        }

        public IEnumerable<SalaryDisbursementDto> ApproveSalaryByBatch(string batchId)
        {
            // No conversion needed - both are strings
            var pendingSalaries = _repo.GetAll()
                .Where(s => s.BatchId == batchId && s.Status == PaymentStatus.Pending)
                .ToList();

            if (!pendingSalaries.Any())
            {
                throw new InvalidOperationException($"No pending salaries found for department: {batchId}");
            }

            var approved = new List<SalaryDisbursementDto>();

            foreach (var salary in pendingSalaries)
            {
                try
                {
                    var emp = _employeeRepo.GetById(salary.EmployeeId);
                    if (emp == null)
                    {
                        Console.WriteLine($"Employee not found for ID: {salary.EmployeeId}");
                        continue;
                    }

                    var clientAccount = _accountRepo.GetByClientId(salary.ClientId);
                    if (clientAccount == null)
                    {
                        Console.WriteLine($"Client account not found for Client ID: {salary.ClientId}");
                        continue;
                    }

                    // Check if account is active
                    if (clientAccount.AccountStatus != AccountStatus.Active)
                    {
                        Console.WriteLine($"Account is not active for Client ID: {salary.ClientId}");
                        continue;
                    }

                    // Check sufficient balance
                    if (clientAccount.Balance < salary.Amount)
                    {
                        Console.WriteLine($"Insufficient balance for Client ID: {salary.ClientId}. Required: {salary.Amount}, Available: {clientAccount.Balance}");
                        continue;
                    }

                    // Process the transaction - deduct amount from employer's account
                    clientAccount.Balance -= salary.Amount;
                    _accountRepo.Update(clientAccount.AccountId, clientAccount);

                    // Update salary status to approved
                    salary.Status = PaymentStatus.Approved;
                    _repo.Update(salary.DisbursementId, salary);

                    // Create transaction record
                    var transaction = new Transaction
                    {
                        AccountId = clientAccount.AccountId,
                        SenderId = salary.ClientId,
                        ReceiverId = emp.EmployeeClientId,
                        Amount = salary.Amount,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionType.Credit,
                        TransactionStatus = TransactionStatus.Success,
                        ReceiverName = emp?.EmployeeClient?.User?.Username ?? "Unknown",
                        SenderName = emp?.EmployerClient?.User?.Username ?? "Unknown",
                        //Description = $"Salary disbursement for {emp.Name} - {emp.Department}"
                    };
                    _transactionRepo.Add(transaction);

                    // Add to approved list
                    approved.Add(new SalaryDisbursementDto
                    {
                        DisbursementId = salary.DisbursementId,
                        EmployeeId = emp.EmployeeId,
                        ClientId = salary.ClientId,
                        EmployeeName = emp?.EmployeeClient?.User?.Username ?? "Unknown",
                        SenderName = emp?.EmployerClient?.User?.Username ?? "Unknown",
                        Amount = salary.Amount,
                        Date = salary.Date,
                        Status = salary.Status,
                        BatchId = emp.Department,
                        //Department = emp.Department
                    });

                    Console.WriteLine($"Successfully processed salary for Employee: {emp.Name} in department: {batchId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing salary ID {salary.DisbursementId}: {ex.Message}");
                    // Continue with next salary even if one fails
                }
            }

            if (!approved.Any())
            {
                throw new InvalidOperationException($"No salaries could be processed for department: {batchId}. Check account status, balances, and employee data.");
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
