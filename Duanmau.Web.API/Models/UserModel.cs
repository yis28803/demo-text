using System.ComponentModel.DataAnnotations;

namespace Duanmau.Web.API.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
