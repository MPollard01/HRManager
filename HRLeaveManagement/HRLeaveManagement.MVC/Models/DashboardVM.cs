namespace HRLeaveManagement.MVC.Models
{
    public class DashboardVM
    {
        public int EmployeeCount { get; set; }
        public int RequestCount { get; set; }
        public int TimeEntryApprovalCount { get; set; }
        public int[] DaysAbscent { get; set; }
        public List<LeaveRequestVM> Requests { get; set; }
    }

    public class EmployeeDashboardVM
    {
        public int RequestCount { get; set; }
        public int TimeEntryApprovalCount { get; set; }
        public int[] MonthlyHours { get; set; }
        public List<LeaveRequestVM> Requests { get; set; }
        public EmployeeVM Employee { get; set; }
    }
}
