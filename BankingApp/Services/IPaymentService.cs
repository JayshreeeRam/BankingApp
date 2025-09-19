using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> GetAll();
        PaymentDto? GetById(int id);
        Payment Add(PaymentDto dto);
        Payment? Update(int id, PaymentDto dto);
        bool Delete(int id);
    }
}
