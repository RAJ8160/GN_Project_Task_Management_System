using Asp.Versioning;
using GN_Project_Task_Management_System.DTOs.RoleDTOs;
using GN_Project_Task_Management_System.DTOs.UsersDTO;
using GN_Project_Task_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GN_Project_Task_Management_System.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    //[Authorize]
    public class RoleAPIController : ControllerBase
    {
        private readonly GnProjectTmsContext _context;

        public RoleAPIController(GnProjectTmsContext context)
        {
            _context = context;
        }

        #region GetAllRoles
        [HttpGet("GetAllRoles")]

        public IActionResult GetAllRoles()
        {
            var roles = _context.Roles.Select(x => new RoleDTO
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                ActiveRole = x.ActiveRole
            }).ToList();
            return Ok(roles);
        }
        #endregion

        #region Get-By-RoleId
        [HttpGet("get-by-id/{id}")]

        public IActionResult GetRoleById(int id)
        {
            var role = _context.Roles.Select(x => new RoleDTO
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                ActiveRole = x.ActiveRole
            }).FirstOrDefault(x => x.RoleId == id);
            if (role == null)
            {
                return NotFound(new { message = $"Role with ID {id} not found." });
            }
            return Ok(role);
        }
        #endregion

        #region Delete Role
        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole(int id)
        {
            if (id == 0)
            {
                return BadRequest(new { message = "Please Enter RoleId here For Delete specific Role" });
            }
            var Role = _context.Roles.FirstOrDefault(x => x.RoleId == id);
            if (Role == null)
            {
                return NotFound(new { message = $"Role with ID {id} not found." });
            }
            Role.ActiveRole = false;
            return NoContent();
        }
        #endregion

        #region InserRole
        [HttpPost("InsertRole")]
        public IActionResult InsertRole(Add_Update_Dto role)
        {
            var role1 = new Role
            {
                RoleName = role.RoleName,
                ActiveRole = role.ActiveRole
            };
            if (role == null)
            {
                return BadRequest(new { message = "Please Enter Proper Data Here." });
            }
            _context.Roles.Add(role1);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region UpdateRole
        [HttpPut("UpdateRole/{id}")]
        public IActionResult EditRole(Add_Update_Dto role, int id)
        {
            var role1 = _context.Roles.FirstOrDefault(y => y.RoleId == id);

            if (role1 == null)
            {
                return NotFound(new { message = "Role Not exist for Updation." });
            }
            role1.RoleName = role.RoleName;
            role1.ActiveRole = role.ActiveRole;
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region RoleDropDown
        [HttpGet("RoleDropDown")]

        public IActionResult RoleDropDown()
        {
            var roleDropDown = _context.Roles.Where(r => r.ActiveRole == true).Select(r => new { r.RoleId, r.RoleName }).ToList();
            return Ok(roleDropDown);
        }
        #endregion

        
    }
}
