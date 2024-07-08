﻿using System;
using System.Collections.Generic;
using SimpleHRToolAPI.Models.EmployeeModel;
using SimpleHRToolAPI.Models.LeaveRequestModels;

namespace SimpleHRToolAPI.Models.ApprovalRequestModel;

public partial class ApprovalRequest
{
    public int Id { get; set; }

    public int Approver { get; set; }

    public int LeaveRequest { get; set; }

    public string Status { get; set; } = null!;

    public string? Comment { get; set; }

    public virtual Employee ApproverNavigation { get; set; } = null!;

    public virtual LeaveRequest LeaveRequestNavigation { get; set; } = null!;
}
