using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int? IssueId { get; set; }

    public int? UserId { get; set; }

    public string ActionType { get; set; } = null!;

    public DateTime? ActionTime { get; set; }

    public bool? ActiveActivityLog { get; set; }

    public virtual Issue? Issue { get; set; }

    public virtual User? User { get; set; }
}
