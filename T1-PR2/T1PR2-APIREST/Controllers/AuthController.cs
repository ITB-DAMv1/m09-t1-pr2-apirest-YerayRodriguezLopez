using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T1PR2_APIREST.DTOs;
using T1PR2_APIREST.Models;

namespace T1PR2_APIREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet("check")]
        public IActionResult CheckToken()
        {
            return Ok(new
            {
                Username = User.Identity?.Name,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Ok("Admin registered successfully.");
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid email or password.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("DisplayName", user.DisplayName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));CreateToken(claims.ToArray());

            return Ok(CreateToken(claims.ToArray()));
        }

        private string CreateToken(Claim[] claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
