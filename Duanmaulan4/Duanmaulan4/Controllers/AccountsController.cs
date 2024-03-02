using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Duanmaulan4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Duanmaulan4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices accountRepository;
        private readonly ILogger<AccountsController> logger;

        public AccountsController(IAccountServices accountRepository, ILogger<AccountsController> logger)
        {
            this.accountRepository = accountRepository;
            this.logger = logger;
        }

        [HttpPost("signup")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<IActionResult> SignUp([FromBody] SignUpAdmin model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.SignUpAsync(model);
                if (result.Succeeded)
                {
                    return Ok(new { Message = "Đăng ký thành công!" });
                }
                else
                {
                    return BadRequest(new { Error = "Đăng ký không thành công", Errors = result.Errors });
                }
            }
            return BadRequest(new { Error = "Dữ liệu không hợp lệ" });
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var token = await accountRepository.SignInAsync(signInModel);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { Error = "Đăng nhập không thành công" });
                }
            }
            return BadRequest(new { Error = "Dữ liệu không hợp lệ" });
        }

        [HttpPost("signin_admin")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<IActionResult> SignIn_Admin([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var token = await accountRepository.SignInAsync(signInModel);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { Error = "Đăng nhập không thành công" });
                }
            }
            return BadRequest(new { Error = "Dữ liệu không hợp lệ" });
        }
        [HttpPost("signin_lecturer")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Lecturer)]
        public async Task<IActionResult> SignIn_Lecturer([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var token = await accountRepository.SignInAsync(signInModel);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { Error = "Đăng nhập không thành công" });
                }
            }
            return BadRequest(new { Error = "Dữ liệu không hợp lệ" });
        }
        [HttpPost("signin_student")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Student)]
        public async Task<IActionResult> SignIn_Student([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var token = await accountRepository.SignInAsync(signInModel);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { Error = "Đăng nhập không thành công" });
                }
            }
            return BadRequest(new { Error = "Dữ liệu không hợp lệ" });
        }

        [HttpPost("signout")]
        /*[Authorize]*/
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Student)]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await accountRepository.SignOutAsync();
                return Ok(new { Message = "Đăng xuất thành công!" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Đăng xuất mém thành công" });
            }
        }



        [HttpPost("forgot-password")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordModel model)
        {
            var token = await accountRepository.GeneratePasswordResetTokenAsync(model.Email);

            if (token != null)
            {
                // Gửi email với đường link đặt lại mật khẩu
                // Đây chỉ là ví dụ đơn giản, bạn cần thực hiện chức năng gửi email thực tế ở đây

                var resetPasswordLink = $"{Request.Scheme}://{Request.Host}/reset-password?email={model.Email}&token={token}";

                var mailContent = new MailContent
                {
                    To = model.Email,
                    Subject = "Reset Password",
                    Body = $"Please click the following link to reset your password: {resetPasswordLink}"
                };

                // Gọi service để gửi email
                await accountRepository.SendMail(mailContent);

                return Ok(new { ResetPasswordLink = resetPasswordLink });
            }
            else
            {
                return BadRequest(new { Error = "Không thể gửi đường link đặt lại mật khẩu" });
            }
        }

        [HttpPost("reset-password")]
        [Authorize(Roles = PhanQuyenViewModel.Role_Admin + "," + PhanQuyenViewModel.Role_Lecturer + "," + PhanQuyenViewModel.Role_Accounting + "," + PhanQuyenViewModel.Role_Director + "," + PhanQuyenViewModel.Role_Registration)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel model)
        {
            try
            {
                await accountRepository.ResetPasswordAsync(model);
                return Ok(new { Message = "Đặt lại mật khẩu thành công!" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
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
                return StatusCode(500, new { Error = "Bạn sai ở chỗ nào đó rồi" });
            }
        }

        [HttpPost("updateuserrole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleModel model)
        {
            try
            {
                await accountRepository.UpdateUserRoleAsync(model.UserId, model.NewRole);
                return Ok(new { Message = "Role được thay đổi thành công" });
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex.Message);
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, new { Error = "Bạn sai ở chỗ nào đó rồi" });
            }
        }





    }
}
