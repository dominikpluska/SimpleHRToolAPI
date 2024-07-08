namespace SimpleHRToolAPI.Models.LeaveRequestModels
{
    public class LeaveRequestDTO
    {
        public int Id { get; set; }
        public string EmployeeFullName { get; set; }

        public string AbsenceReason { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
