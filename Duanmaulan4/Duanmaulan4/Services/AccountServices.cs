using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Duanmaulan4.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountServices> logger;
        private readonly ApplicationDbContext context;

        public AccountServices(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager,
            ILogger<AccountServices> logger,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.logger = logger;
            this.context = context;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpAdmin model)
        {
            // Tạo một đối tượng ApplicationUser từ dữ liệu đăng ký
            var user = new ApplicationUser
            {
                Ho = model.Ho,
                TenDemvaTen = model.TenDemvaTen,
                Email = model.Email,
                UserName = model.Email,
                ImageUrl = model.ImageUrl
            };

            // Thực hiện đăng ký người dùng
            var result = await userManager.CreateAsync(user, model.Password);

            // Nếu đăng ký thành công
            if (result.Succeeded)
            {
                // Kiểm tra và thêm vai trò Admin nếu chưa có
                if (!await roleManager.RoleExistsAsync(PhanQuyenViewModel.Role_Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(PhanQuyenViewModel.Role_Admin));
                }

                // Thêm người dùng vào vai trò Admin
                await userManager.AddToRoleAsync(user, PhanQuyenViewModel.Role_Admin);
            }

            // Trả về kết quả của quá trình đăng ký
            return result;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !passwordValid)
            {
                var errorMessage = "Đăng nhập không thành công. Tên người dùng hoặc mật khẩu không đúng.";
                return $"{{\"Error\": \"{errorMessage}\"}}";
            }


            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id) // Thêm claim ID ở đây
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            user.ResetToken = resetToken;
            await userManager.UpdateAsync(user);

            return resetToken;
        }

        public async Task ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                throw new NotFoundException("User not found.");
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                // Xử lý khi không thể đặt lại mật khẩu
                throw new Exception("Failed to reset password.");
            }
        }
        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(configuration["MailSettings:DisplayName"], configuration["MailSettings:Mail"]);
            email.From.Add(new MailboxAddress(configuration["MailSettings:DisplayName"], configuration["MailSettings:Mail"]));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // Dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
                smtp.Authenticate(configuration["MailSettings:Mail"], configuration["MailSettings:Password"]);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);

                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            logger.LogInformation("send mail to " + mailContent.To);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailContent = new MailContent
            {
                To = email,
                Subject = subject,
                Body = htmlMessage
            };

            await SendMail(mailContent);
        }
        
        public async Task<string> GetUserInfoAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                throw new NotFoundException("User not found.");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var rolesInfo = new List<object>();

            foreach (var roleName in userRoles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    rolesInfo.Add(new
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    });
                }
            }

            return JsonConvert.SerializeObject(new
            {
                user.Id,
                user.Ho,
                user.TenDemvaTen,
                user.Email,
                Roles = rolesInfo
                // Các thông tin khác mà bạn muốn hiển thị
            });
        }
        
        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                throw new NotFoundException("User not found.");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            if (!userRoles.Contains(newRole))
            {
                // Lưu trữ vai trò cũ để kiểm tra thay đổi
                var oldRole = userRoles.FirstOrDefault();

                await userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
                await userManager.AddToRoleAsync(user, newRole);

                // Kiểm tra xem vai trò đã thay đổi từ Student thành Lecturer hay không
                if (oldRole == AppRole.Student && newRole == AppRole.Lecturer)
                {
                    // Xóa thông tin người dùng từ bảng Student
                    await RemoveStudentInfoAsync(userId);
                }

                // Có thể thêm xử lý thông báo hoặc kết quả tại đây
                logger.LogInformation($"Cập nhật vai trò người dùng thành công: {user.Email} - Vai trò mới: {newRole}");
            }
        }

        private async Task RemoveStudentInfoAsync(string userId)
        {
            // Thực hiện xóa thông tin người dùng từ bảng Student ở đây
            // Ví dụ:
            var student = await context.HocSinh.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student != null)
            {
                context.HocSinh.Remove(student);
                await context.SaveChangesAsync();
                logger.LogInformation($"Xóa thông tin người dùng từ bảng Student thành công: {userId}");
            }
        }

    }
}
