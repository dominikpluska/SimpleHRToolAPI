namespace SimpleHRToolAPI.Models.ProjectModel
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string ProjectType { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string ProjectManagerFullName { get; set; }

        public string Status { get; set; } = null!;
    }
}
