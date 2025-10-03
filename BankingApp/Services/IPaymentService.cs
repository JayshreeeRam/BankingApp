using BankingApp.DTOs;
using BankingApp.Models;

public interface IPaymentService
{
    IEnumerable<PaymentDto> GetAll();
    PaymentDto? GetById(int id);
    PaymentDto Add(CreatePaymentDto dto);     
    PaymentDto? Update(int id, PaymentDto dto);
    //bool Delete(int id);
    PaymentDto? ApprovePayment(int id);
    PaymentDto? RejectPayment(int id,string remark);
}
