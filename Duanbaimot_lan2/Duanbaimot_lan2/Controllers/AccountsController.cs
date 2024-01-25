using Duanbaimot_lan2.Data;
using Duanbaimot_lan2.Models;
using Duanbaimot_lan2.Repositories;
using Duanbaimot_lan2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Duanbaimot_lan2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ISendMailService sendMailService; // Thêm dòng này

        public AccountsController(IAccountRepository repo, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ISendMailService sendMailService)
        {
            accountRepo = repo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.sendMailService = sendMailService; // Thêm dòng này
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                // Gửi email sau khi đăng ký thành công
                var mailContent = new MailContent
                {
                    To = signUpModel.Email,
                    Subject = "Welcome to our application",
                    Body = "Thank you for signing up!"
                };

                await sendMailService.SendMail(mailContent);

                return Ok(new { Message = "Sign up successful" });
            }

            // Include more details in the response body for error cases
            return BadRequest(new { Message = "Sign up failed", Errors = result.Errors });
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            // Gửi email sau khi đăng nhập thành công
            var mailContent = new MailContent
            {
                To = signInModel.Email,
                Subject = "Login Notification",
                Body = "You have successfully logged in."
            };

            await sendMailService.SendMail(mailContent);

            return Ok(result);
        }

        [HttpPost("SignOut")]
        [Authorize] // Đảm bảo chỉ có người dùng đã đăng nhập mới có thể đăng xuất
        public async Task<IActionResult> SignOut()
        {
            await accountRepo.SignOutAsync();
            return Ok(new { Message = "Sign out successful" });
        }







        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            var resetToken = await accountRepo.GeneratePasswordResetTokenAsync(model.Email);

            if (resetToken != null)
            {
                // Gửi email chứa đường dẫn để đặt lại mật khẩu
                var resetUrl = $"{Request.Scheme}://{Request.Host}/api/Accounts/ResetPassword?email={model.Email}&token={resetToken}";

                var mailContent = new MailContent
                {
                    To = model.Email,
                    Subject = "Reset Password",
                    Body = $"Please click the following link to reset your password: {resetUrl}"
                };

                await sendMailService.SendMail(mailContent);

                return Ok(new { Message = "Reset password link has been sent to your email." });
            }

            return BadRequest(new { Message = "Failed to send reset password link." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset successfully." });
            }

            return BadRequest(new { Message = "Failed to reset password.", Errors = result.Errors });
        }





    }

}
