using Employee_API_JWT.Identity;
using Employee_API_JWT.Models.ViewModels;
using Employee_API_JWT.ServiceContact;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_API_JWT.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly AppSettings _appSettings;
        public UserService(ApplicationSignInManager applicationSignInManager,ApplicationUserManager applicationUserManager,IOptions<AppSettings> appSettings)
        {
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _appSettings = appSettings.Value;
        }
        public async Task<ApplicationUser> Authenticate(LoginVM loginVM)
        {
            var result = await _applicationSignInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginVM.UserName);
                applicationUser.PasswordHash = "";
                //JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Employee))
                    applicationUser.Role = SD.Role_Employee;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDiscriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, applicationUser.Id),
                    new Claim(ClaimTypes.Email,applicationUser.Email),
                    new Claim(ClaimTypes.Role, applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDiscriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);
                //**
                return applicationUser;
            }
            return null;
        }

        public async Task<IdentityResult> Register(RegisterVM registerVM)
        {
            var user = new ApplicationUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };

            // Step 1: Create user
            var result = await _applicationUserManager.CreateAsync(
                user,
                registerVM.Password
            );

            if (!result.Succeeded)
            {
                return result;
            }

            // Step 2: Assign Employee role
            var roleResult = await _applicationUserManager.AddToRoleAsync(
                user,
                SD.Role_Employee
            );

            if (!roleResult.Succeeded)
            {
                await _applicationUserManager.DeleteAsync(user);
                return roleResult;
            }

            return IdentityResult.Success;
        }
        public async Task<ProfileVM> GetProfile(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var profile = new ProfileVM()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return profile;
        }
        public async Task<IdentityResult> UpdateProfile(string userId, ProfileVM profileVM)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "User not found" }
                );
            }

            user.UserName = profileVM.UserName;
            user.Email = profileVM.Email;
            user.PhoneNumber = profileVM.PhoneNumber;

            var result = await _applicationUserManager.UpdateAsync(user);

            return result;
        }
        public async Task<IdentityResult> ChangePassword(
    string userId,
    ChangePasswordVM changePasswordVM)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "User not found" }
                );
            }

            var result = await _applicationUserManager.ChangePasswordAsync(
                user,
                changePasswordVM.CurrentPassword,
                changePasswordVM.NewPassword
            );

            return result;
        }
    }
}
