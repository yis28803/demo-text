using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.DataView.Authentication
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
