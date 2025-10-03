using System.Net;
using System.Net.Mail;

namespace BankingApp.Services
{
  
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendRejectionEmail(string clientEmail, string clientName, string remark)
        {
            try
            {
                // Configure your email settings in appsettings.json
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = _configuration["EmailSettings:Port"];
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                using (var client = new SmtpClient(smtpServer, int.Parse(port)))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(username, password);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("noreply@bankingapp.com", "Banking App"),
                        Subject = "Client Registration Rejected",
                        Body = GenerateRejectionEmailBody(clientName, remark),
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(clientEmail);

                    client.Send(mailMessage);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        private string GenerateRejectionEmailBody(string clientName, string remark)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background: #f8f9fa; padding: 20px; text-align: center; }}
                    .content {{ padding: 20px; }}
                    .footer {{ background: #f8f9fa; padding: 10px; text-align: center; font-size: 12px; color: #6c757d; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Banking Application</h2>
                    </div>
                    <div class='content'>
                        <h3>Dear {clientName},</h3>
                        <p>We regret to inform you that your client registration has been rejected.</p>
                        <div style='background: #fff3cd; padding: 15px; border-left: 4px solid #ffc107; margin: 15px 0;'>
                            <strong>Reason for Rejection:</strong>
                            <p>{remark}</p>
                        </div>
                        <p>If you have any questions or would like to submit additional information, please contact our support team.</p>
                        <p>Best regards,<br>Banking App Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}
