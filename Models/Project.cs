using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? ActiveProject { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
}
