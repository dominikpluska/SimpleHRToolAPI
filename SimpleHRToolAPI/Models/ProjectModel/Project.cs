using System;
using System.Collections.Generic;
using SimpleHRToolAPI.Models.EmployeeModel;

namespace SimpleHRToolAPI.Models.ProjectModel;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int ProjectManager { get; set; }

    public string? Comment { get; set; }

    public string Status { get; set; } = null!;

    public string ProjectManagerFullName { get; set; }

    public virtual Employee ProjectManagerNavigation { get; set; } = null!;
}
