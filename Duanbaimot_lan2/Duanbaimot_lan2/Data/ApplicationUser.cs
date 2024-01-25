using Microsoft.AspNetCore.Identity;

namespace Duanbaimot_lan2.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<Class> Classes { get; set; }
        public ICollection<Student> Students { get; set; }
        public string ResetToken { get; set; }
    }
}
