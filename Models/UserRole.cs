using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public bool? ActiveUserRole { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
