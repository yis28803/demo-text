using Duanmau.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Duanmau.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DuanmauDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(DuanmauDbContext dbContext, IConfiguration configuration, ILogger<AccountController> logger)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            try
            {
                // Kiểm tra xem tên người dùng đã tồn tại chưa
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userModel.UserName);
                if (existingUser != null)
                {
                    return BadRequest("Tên người dùng đã tồn tại");
                }

                // Hash mật khẩu và lưu vào cơ sở dữ liệu
                userModel.HashedPassword = HashPassword(userModel.Password);
                _dbContext.Users.Add(userModel);
                await _dbContext.SaveChangesAsync();

                return Ok("Đăng ký thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                // Log giá trị loginModel.UserName và loginModel.Password để kiểm tra
                _logger.LogInformation($"Login attempt with UserName: {loginModel.UserName}, Password: {loginModel.Password}");
                // Kiểm tra xem tên người dùng và mật khẩu có khớp không
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == loginModel.UserName && u.HashedPassword == HashPassword(loginModel.Password));

                if (user == null)
                {
                    _logger.LogWarning($"Login failed for UserName: {loginModel.UserName}");
                    return Unauthorized("Đăng nhập không thành công");
                }

                // Tạo JWT token và trả về thông tin người dùng đã đăng nhập thành công và JWT token
                var token = GenerateJwtToken(user);
                return Ok(new { UserId = user.UserId, UserName = user.UserName, Role = user.Role, Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }


        private string HashPassword(string password)
        {
            // Thực hiện logic hash mật khẩu ở đây (ví dụ: sử dụng bcrypt, PBKDF2, etc.)
            // Lưu ý: Không nên lưu mật khẩu dưới dạng văn bản, luôn sử dụng hash
            return password; // Đây chỉ là ví dụ đơn giản, không nên sử dụng trong môi trường thực tế
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
