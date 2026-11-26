using Employee_API_JWT.Identity;
using Employee_API_JWT.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Employee_API_JWT.ServiceContact
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(LoginVM loginVM);
        Task<IdentityResult> Register(RegisterVM registerVM);
        Task<ProfileVM> GetProfile(string userId);
        Task<IdentityResult> UpdateProfile(string userId, ProfileVM profileVM);
        Task<IdentityResult> ChangePassword(string userId, ChangePasswordVM changePasswordVM);
    }
}
