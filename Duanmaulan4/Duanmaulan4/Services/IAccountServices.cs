using Duanmaulan4.DataView.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Services
{
    public interface IAccountServices
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model, int maLop);
        Task<string> SignInAsync(SignInModel model);
        Task SignOutAsync();
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task ResetPasswordAsync(ResetPasswordModel model);

        Task SendMail(MailContent mailContent);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task<string> GetUserInfoAsync(string userId);
        Task UpdateUserRoleAsync(string userId, string newRole);
    }
}
