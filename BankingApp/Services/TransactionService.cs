//using BankingApp.DTOs;
//using BankingApp.Models;
//using BankingApp.Repository;
//using BankingApp.Enums;
//using Microsoft.AspNetCore.Http;
//using System.Text.Json;

//namespace BankingApp.Services
//{
//    public class TransactionService : ITransactionService
//    {
//        private readonly ITransactionRepository _repo;
//        private readonly IAuditService _audit;
//        private readonly IHttpContextAccessor _httpContext;

//        public TransactionService(
//            ITransactionRepository repo,
//            IAuditService audit,
//            IHttpContextAccessor httpContext)
//        {
//            _repo = repo;
//            _audit = audit;
//            _httpContext = httpContext;
//        }

//        public TransactionDto Register(RegisterTransactionDto dto)
//        {
//            var transaction = new Transaction
//            {
//                AccountId = dto.AccountId,
//                Amount = dto.Amount,
//                TransactionType = Enum.Parse<TransactionType>(dto.TransactionType, true),
//                TransactionStatus = Enum.Parse<TransactionStatus>(dto.TransactionStatus, true),
//                TransactionDate = DateTime.UtcNow
//            };

//            _repo.Add(transaction);

//            _audit.Log(new CreateAuditLogDto
//            {
//                UserId = GetCurrentUserId(),
//                Action = "CREATE_TRANSACTION",
//                EntityName = nameof(Transaction),
//                EntityId = transaction.TransactionId,
//                OldValueJson = null,
//                NewValueJson = JsonSerializer.Serialize(transaction),
//                IpAddress = GetClientIp()
//            });

//            return MapToDto(transaction);
//        }

//        public TransactionDto? UpdateTransaction(int id, UpdateTransactionDto dto)
//        {
//            var transaction = _repo.GetById(id);
//            if (transaction == null) return null;

//            var oldValue = JsonSerializer.Serialize(transaction);

//            transaction.Amount = dto.Amount ?? transaction.Amount;
//            if (!string.IsNullOrEmpty(dto.TransactionType))
//                transaction.TransactionType = Enum.Parse<TransactionType>(dto.TransactionType, true);
//            if (!string.IsNullOrEmpty(dto.TransactionStatus))
//                transaction.TransactionStatus = Enum.Parse<TransactionStatus>(dto.TransactionStatus, true);

//            _repo.Update(transaction);

//            _audit.Log(new CreateAuditLogDto
//            {
//                UserId = GetCurrentUserId(),
//                Action = "UPDATE_TRANSACTION",
//                EntityName = nameof(Transaction),
//                EntityId = transaction.TransactionId,
//                OldValueJson = oldValue,
//                NewValueJson = JsonSerializer.Serialize(transaction),
//                IpAddress = GetClientIp()
//            });

//            return MapToDto(transaction);
//        }

//        public bool SoftDeleteTransaction(int id)
//        {
//            var transaction = _repo.GetById(id);
//            if (transaction == null) return false;

//            var oldValue = JsonSerializer.Serialize(transaction);

//            transaction.TransactionStatus = TransactionStatus.Cancelled;
//            _repo.Update(transaction);

//            _audit.Log(new CreateAuditLogDto
//            {
//                UserId = GetCurrentUserId(),
//                Action = "SOFT_DELETE_TRANSACTION",
//                EntityName = nameof(Transaction),
//                EntityId = transaction.TransactionId,
//                OldValueJson = oldValue,
//                NewValueJson = JsonSerializer.Serialize(transaction),
//                IpAddress = GetClientIp()
//            });

//            return true;
//        }

//        public TransactionDto? GetById(int id)
//        {
//            var transaction = _repo.GetById(id);
//            return transaction == null ? null : MapToDto(transaction);
//        }

//        public IEnumerable<TransactionDto> GetByAccountId(int accountId)
//        {
//            var transactions = _repo.GetByAccountId(accountId);
//            return transactions.Select(MapToDto).ToList();
//        }

//        // 🔹 Helpers
//        private TransactionDto MapToDto(Transaction transaction) =>
//            new TransactionDto
//            {
//                TransactionId = transaction.TransactionId,
//                AccountId = transaction.AccountId,
//                Amount = transaction.Amount,
//                TransactionType = transaction.TransactionType.ToString(),
//                TransactionStatus = transaction.TransactionStatus.ToString(),
//                TransactionDate = transaction.TransactionDate
//            };

//        private int GetCurrentUserId()
//        {
//            var userIdClaim = _httpContext.HttpContext?.User?.FindFirst("userId")?.Value;
//            return int.TryParse(userIdClaim, out var id) ? id : 0;
//        }

//        private string GetClientIp()
//        {
//            return _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
//        }
//    }
//}
