using Employee_API_JWT.Models.ViewModels;
using Employee_API_JWT.ServiceContact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Employee_API_JWT.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginVM loginVM)
        {
            var user = await _userService.Authenticate(loginVM);
            if (user == null) return BadRequest("Wrong user / pwd");
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(registerVM);

            if (!result.Succeeded)
            {
                return BadRequest(
                    result.Errors.Select(e => e.Description)
                );
            }

            return Ok("User registered successfully");
        }
        [Authorize]
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var profile = await _userService.GetProfile(userId);

            if (profile == null)
            {
                return NotFound("User not found");
            }

            return Ok(profile);
        }
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileVM profileVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.Name);

            var result = await _userService.UpdateProfile(userId, profileVM);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok("Profile updated successfully");
        }
        [Authorize]
        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(
     string userId,
     [FromBody] ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ChangePassword(
                userId,
                changePasswordVM
            );

            if (!result.Succeeded)
            {
                return BadRequest(
                    result.Errors.Select(e => e.Description)
                );
            }

            return Ok("Password changed successfully");
        }
    }

}
