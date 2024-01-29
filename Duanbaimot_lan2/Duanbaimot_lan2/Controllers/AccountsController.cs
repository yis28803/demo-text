using Duanbaimot_lan2.Helpers;
using Duanbaimot_lan2.Models;
using Duanbaimot_lan2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Duanbaimot_lan2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices accountRepository;
        private readonly ILogger<AccountsController> logger;

        public AccountsController(IAccountServices accountRepository, ILogger<AccountsController> logger)
        {
            this.accountRepository = accountRepository;
            this.logger = logger;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            try
            {
                var token = await accountRepository.SignInAsync(model);
                return Ok(new { Token = token });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            try
            {
                var result = await accountRepository.SignUpAsync(model);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Sign up successful" });
                }
                else
                {
                    return BadRequest(new { Error = "Sign up failed", Errors = result.Errors });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await accountRepository.SignOutAsync();
                return Ok(new { Message = "Sign out successful" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpPost("resetpassword/request")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestModel model)
        {
            try
            {
                var resetToken = await accountRepository.GeneratePasswordResetTokenAsync(model.Email);

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

                    // Gọi service để gửi email
                    await accountRepository.SendMail(mailContent);

                    return Ok(new { Message = "Reset password link has been sent to your email." });
                }

                return BadRequest(new { Message = "Failed to send reset password link." });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                await accountRepository.ResetPasswordAsync(model);
                return Ok(new { Message = "Password reset successful" });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpGet("userinfo/{userId}")]
        public async Task<IActionResult> GetUserInfo(string userId)
        {
            try
            {
                var userInfo = await accountRepository.GetUserInfoAsync(userId);
                return Ok(userInfo);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        [HttpPost("updateuserrole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleModel model)
        {
            try
            {
                await accountRepository.UpdateUserRoleAsync(model.UserId, model.NewRole);
                return Ok(new { Message = "User role updated successfully" });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

    }
}
