using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using GN_Project_Task_Management_System.DTOs.UsersDTO;
using GN_Project_Task_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GN_Project_Task_Management_System.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    //[Authorize]
    public class UsersAPIController : ControllerBase
    {
        private readonly GnProjectTmsContext _context;

        public UsersAPIController(GnProjectTmsContext context)
        {
            _context = context;
        }

        #region Select All Users

        [HttpGet]

        public IActionResult GetAllUsers()
        {
            var usersList = _context.Users
                             //.Where(u => u.ActiveUser == true)  // Optional: filter active users only
                             .Select(u => new UserDto
                             {
                                 UserId = u.UserId,
                                 UserName = u.UserName,
                                 Email = u.Email,
                                 PasswordHash = u.PasswordHash,
                                 CreatedAt = u.CreatedAt,
                                 ActiveUser = u.ActiveUser
                             })
                             .ToList();
            return Ok(usersList);
        }
        #endregion

        #region Select User By ID

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users
                .Where(u => u.UserId == id && u.ActiveUser == true)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        #endregion

        #region Delete User
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            user.ActiveUser = false;
            _context.SaveChanges();

            return Ok($"User with ID {id} deleted successfully.");
        }
        #endregion

        #region Add New User
        [HttpPost]

        public IActionResult addNewUser(AddUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new { message = "Enter Proper data for Insert New User." });
            }
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                CreatedAt = DateTime.Now,
                ActiveUser = true
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Update User
        [HttpPut("{id}")]

        public IActionResult updateUser(AddUserDto user, int id)
        {
            var existinguser = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (existinguser == null)
            {
                return NotFound(new { message = "User not found For updation." });
            }

            existinguser.UserName = user.UserName;
            existinguser.Email = user.Email;
            existinguser.PasswordHash = user.PasswordHash;

            _context.Users.Update(existinguser);
            _context.SaveChanges();
            return NoContent();
        }

        #endregion

        #region User DropDown
        [HttpGet("UserDropDown")]

        public IActionResult UserDropDown()
        {
            var userDropDown = _context.Users.Where(u => u.ActiveUser == true).Select(u => new { u.UserId, u.UserName }).ToList();
            return Ok(userDropDown);
        }
        #endregion
    }
}