using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using polisync.Data;
using polisync.Models;
using polisync.Models.Login;

namespace polisync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private AppDbContext _context;
        private IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // === Endpoints ===
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginEndpoint([FromBody] LoginRequest loginRequest)
        {
            var user = AuthenticateUser(loginRequest);

            if (user == null)
                return NotFound("User not found.");

            // Cookie based auth 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return Ok(new 
                { 
                    message = "Login successful.", 
                    role = user.Role.ToString()
                });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutEndpoint()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return Ok();
        }

        // === Helper Methods ===
        private UserModel? AuthenticateUser(LoginRequest loginRequest)
        {
            var currentUser = _context.Users.FirstOrDefault(u =>
                u.Email == loginRequest.Email
                &&
                u.Password == loginRequest.Password
            );
            
            if (currentUser == null)
                return null;

            return currentUser;
        }

        /* === Replaced JWT auth with cookie based auth ===

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.Name),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        */

        
    }
}