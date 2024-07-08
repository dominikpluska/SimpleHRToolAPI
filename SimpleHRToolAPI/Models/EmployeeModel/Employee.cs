using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleHRToolAPI.Models.ApprovalRequestModel;
using SimpleHRToolAPI.Models.LeaveRequestModels;
using SimpleHRToolAPI.Models.ProjectModel;
using SimpleHRToolAPI.Models.UserModel;

namespace SimpleHRToolAPI.Models.EmployeeModel;

public partial class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Subdivision { get; set; } = null!;

    public string Position { get; set; } = null!;

    public bool Status { get; set; }

    public int? PeoplePartner { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public string? Photo { get; set; }

    public string? PeoplePartnerFullName { get; set; }


    public virtual ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();

    public virtual ICollection<Employee> InversePeoplePartnerNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual Employee? PeoplePartnerNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
