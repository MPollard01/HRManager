

using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.MVC.Models
{
    public class TimeEntryVM : CreateTimeEntryVM
    {
        public int Id { get; set; }
        public bool? Approved { get; set; }
        public int TotalHours { get; set; }
    }

    public class CreateTimeEntryVM
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public List<HoursDay> Hours { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class TimeEntryWithTemplateVM
    {
        [ValidateNever]
        public TimeEntryVM TimeEntry { get; set; }
        [ValidateNever]
        public TemplateTimeVM TemplateTime { get; set; }
    }

    public class AdminTimeEntryVM : TimeEntryVM
    {
        public EmployeeVM Employee { get; set; }
    }

    public class AdminTimeEntryViewVM
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }
        public List<AdminTimeEntryVM> Entries { get; set; }
    }
}
