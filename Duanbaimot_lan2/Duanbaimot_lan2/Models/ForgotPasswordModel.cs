using System.ComponentModel.DataAnnotations;

namespace Duanbaimot_lan2.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        public string? Email { get; set; }
    }
}
