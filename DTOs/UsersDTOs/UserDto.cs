using System.ComponentModel.DataAnnotations;

namespace GN_Project_Task_Management_System.DTOs.UsersDTO
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public bool? ActiveUser { get; set; }
    }
}
