

using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HRLeaveManagement.MVC.Models
{
    public class TimeEntryVM : CreateTimeEntryVM
    {
        public int Id { get; set; }
        //public bool? Approved { get; set; }
        //public EmployeeVM Employee { get; set; }
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
}
