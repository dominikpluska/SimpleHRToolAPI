namespace SimpleHRToolAPI.Models.ApprovalRequestModel
{
    public class ApprovalRequestDTO
    {
        public int Id { get; set; }

        public string Approver { get; set; }

        public string LeaveRequest { get; set; }

        public string Status { get; set; } = null!;


    }
}
