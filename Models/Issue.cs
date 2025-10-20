using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class Issue
{
    public int IssueId { get; set; }

    public int? ProjectId { get; set; }

    public int? SprintId { get; set; }

    public int? TypeId { get; set; }

    public int? AssignedTo { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? ActiveIssue { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual User? AssignedToNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Project? Project { get; set; }

    public virtual Sprint? Sprint { get; set; }

    public virtual IssueType? Type { get; set; }
}
