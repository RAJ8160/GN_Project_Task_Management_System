namespace GN_Project_Task_Management_System.DTOs.UsersDTO
{
    public class AddUserDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public bool? ActiveUser { get; set; }
    }
}
