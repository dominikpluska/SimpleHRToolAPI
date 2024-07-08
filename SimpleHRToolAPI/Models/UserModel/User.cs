using SimpleHRToolAPI.Models.EmployeeModel;
using System;
using System.Collections.Generic;

namespace SimpleHRToolAPI.Models.UserModel;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? PasswordHash { get; set; }

    public int? Employee { get; set; }

    public int? Role { get; set; }

    public virtual Employee? EmployeeNavigation { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
