using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        UserManager<IdentityUser> userManager,
        NZWalksAuthDbContext context,
        ITokenRepository tokenRepository
    ) : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly NZWalksAuthDbContext context = context;
        private readonly ITokenRepository tokenRepository = tokenRepository;

        // Create new user
        // POST: /api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var newUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.EmailAddress
            };

            var dbTransaction = context.Database.BeginTransaction();

            try
            {
                await userManager.CreateAsync(newUser, registerRequest.Password);

                await userManager.AddToRoleAsync(newUser, registerRequest.Role);

                context.SaveChanges();
                dbTransaction.Commit();

                return Ok("User registered Successfully");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                return BadRequest(new { error = ex.Message });
            }
        }

        // Login
        // POST: /api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.EmailAddress);

            if (user != null)
            {
                var isMatch = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (isMatch)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var token = tokenRepository.CreateJWTToken(user, [.. roles]);

                    var response = new LoginResponseDto { Token = token };
                    return Ok(response);
                }
            }

            return BadRequest("Invalid Email or Password!");
        }
    }
}
