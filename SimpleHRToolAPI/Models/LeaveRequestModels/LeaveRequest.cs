using System;
using System.Collections.Generic;
using SimpleHRToolAPI.Models.ApprovalRequestModel;
using SimpleHRToolAPI.Models.EmployeeModel;

namespace SimpleHRToolAPI.Models.LeaveRequestModels;

public partial class LeaveRequest
{
    public int Id { get; set; }

    public int Employee { get; set; }

    public string AbsenceReason { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Comment { get; set; }

    public string Status { get; set; } = null!;

    public string EmployeeFullName { get; set; }

    public virtual ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();

    public virtual Employee EmployeeNavigation { get; set; } = null!;
}
