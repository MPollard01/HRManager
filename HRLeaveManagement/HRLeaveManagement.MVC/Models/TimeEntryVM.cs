namespace HRLeaveManagement.MVC.Models
{
    public class TimeEntryVM : CreateTimeEntryVM
    {
        public int Id { get; set; }
        public bool? Approved { get; set; }
        public EmployeeVM Employee { get; set; }
    }

    public class CreateTimeEntryVM
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public List<HoursDayVM> Hours { get; set; }
    }
}
