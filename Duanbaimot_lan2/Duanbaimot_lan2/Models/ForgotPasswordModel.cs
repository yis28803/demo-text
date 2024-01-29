using System.ComponentModel.DataAnnotations;

namespace Duanbaimot_lan2.Models
{
    public class ResetPasswordRequestModel
    {
        [Required]
        public string? Email { get; set; }
    }
}
