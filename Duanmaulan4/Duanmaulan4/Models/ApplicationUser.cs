using Microsoft.AspNetCore.Identity;

namespace Duanmaulan4.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Ho { get; set; }
        public string? TenDemvaTen { get; set; }
        public string? DienThoai { get; set; }
        public string? ResetToken { get; set; }
    }
}
