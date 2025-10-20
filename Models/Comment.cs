using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? IssueId { get; set; }

    public int? UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? ActiveComment { get; set; }

    public virtual Issue? Issue { get; set; }

    public virtual User? User { get; set; }
}
