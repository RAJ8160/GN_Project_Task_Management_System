using GN_Project_Task_Management_System.DTOs.UsersDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GN_Project_Task_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using GN_Project_Task_Management_System.DTOs;
using Asp.Versioning;

namespace GN_Project_Task_Management_System.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserAuthAPIController : ControllerBase
    {

        private readonly GnProjectTmsContext _context;
        private readonly IConfiguration _configuration;

        public UserAuthAPIController(GnProjectTmsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // 🔑 Generate Token with Role & Expiry from appsettings.json
        private string GenerateJwtToken(RegisterUserDTO user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            var expiryMinutes = Convert.ToDouble(jwtSettings["TokenExpiryMinutes"]);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        #region✅ LOGIN API
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.UserName))
                return BadRequest(new { message = "Username or Email is required" });

            var user = await _context.Users.Join(_context.UserRoles,
               u => u.UserId,
               ur => ur.UserId,
               (u, ur) => new { u, ur })
               .Join(_context.Roles,
               combined => combined.ur.RoleId,
               r => r.RoleId,
               (combined, r) => new { combined.u, Role = r })
                .Where(x => x.u.ActiveUser == true &&
            x.u.PasswordHash == loginUser.PasswordHash &&
            (
                !string.IsNullOrEmpty(loginUser.UserName) && x.u.UserName == loginUser.UserName ||
                !string.IsNullOrEmpty(loginUser.UserName) && x.u.Email == loginUser.UserName
            )).
                Select(x => new RegisterUserDTO
                {
                    UserId = x.u.UserId,
                    UserName = x.u.UserName,
                    PasswordHash = x.u.PasswordHash,
                    Email = x.u.Email,
                    RoleName = x.Role.RoleName,
                }).FirstOrDefaultAsync();
            //.FirstOrDefaultAsync(u => u.UserName == loginUser.UserName && u.PasswordHash == loginUser.PasswordHash);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user
            });
        }
        #endregion

        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO u)
        {
            // ✅ Add input validation
            if (string.IsNullOrEmpty(u.Email) || string.IsNullOrEmpty(u.PasswordHash))
            {
                return BadRequest(new { message = "Email and password are required" });
            }

            // ✅ Check if user already exists
            if (_context.Users.Any(x => x.Email == u.Email))
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            var newUser = new User
            {
                UserName = u.UserName,
                Email = u.Email,
                PasswordHash = u.PasswordHash, // ✅ Hash password before storing
                ActiveUser = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var role = _context.Roles.FirstOrDefault(r => r.RoleName == u.RoleName);
            if (role == null)
                return BadRequest(new { message = "Invalid role name" });

            var userRole = new UserRole
            {
                UserId = newUser.UserId,
                RoleId = role.RoleId
            };

            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            // ✅ Don't expose sensitive data in response
            return Ok(new
            {
                message = "User registered successfully",
                user = new
                {
                    userId = newUser.UserId,
                    name = newUser.UserName,
                    email = newUser.Email,
                    password = u.PasswordHash,
                }
            });
        }
    }
}
