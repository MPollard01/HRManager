namespace HRLeaveManagement.MVC.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class EmployeeDetailsVM
    {
        public EmployeeVM Employee { get; set; }
        public int NumberOfDays { get; set; }
        public bool HasRequests { get; set; }
    }
}
