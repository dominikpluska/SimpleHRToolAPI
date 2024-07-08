namespace SimpleHRToolAPI.Models
{
    public class LoggedSessions
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string JWT { get; set; }
    }
}
