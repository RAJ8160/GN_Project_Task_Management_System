using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool? ActiveRole { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
