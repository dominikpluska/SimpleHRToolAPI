namespace SimpleHRToolAPI.Models.EmployeeModel
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Subdivision { get; set; } = null!;

        public string Position { get; set; } = null!;
        public string? PeoplePartnerFullName { get; set; }
        public bool Status { get; set; }


    }
}
