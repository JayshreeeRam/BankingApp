namespace BankingApp.Services
{
    public interface IEmailService
    {
        bool SendRejectionEmail(string clientEmail, string clientName, string remark);
    }
}
