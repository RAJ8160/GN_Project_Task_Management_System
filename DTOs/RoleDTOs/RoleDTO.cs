namespace GN_Project_Task_Management_System.DTOs.RoleDTOs
{
    public class RoleDTO
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public bool? ActiveRole { get; set; }
    }
}
