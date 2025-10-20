using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GN_Project_Task_Management_System.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? ActiveUser { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
