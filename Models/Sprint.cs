using System;
using System.Collections.Generic;

namespace GN_Project_Task_Management_System.Models;

public partial class Sprint
{
    public int SprintId { get; set; }

    public int? ProjectId { get; set; }

    public string SprintName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Status { get; set; }

    public bool? ActiveSprint { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Project? Project { get; set; }
}
