using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class IssueType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public bool? ActiveType { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
