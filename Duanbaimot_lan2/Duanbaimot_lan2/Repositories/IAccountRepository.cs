using Duanbaimot_lan2.Models;
using Microsoft.AspNetCore.Identity;

namespace Duanbaimot_lan2.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        public Task SignOutAsync();
        public Task<string> GeneratePasswordResetTokenAsync(string email);

    }
}
