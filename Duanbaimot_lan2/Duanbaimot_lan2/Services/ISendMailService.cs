using Duanbaimot_lan2.Models;

namespace Duanbaimot_lan2.Services
{
    public interface ISendMailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
