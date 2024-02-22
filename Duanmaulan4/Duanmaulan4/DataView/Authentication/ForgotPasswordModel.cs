using System.ComponentModel.DataAnnotations;

namespace Duanmaulan4.DataView.Authentication
{
    public class ForgotPasswordModel
    {
        [Required]
        public string? Email { get; set; }
    }
}
