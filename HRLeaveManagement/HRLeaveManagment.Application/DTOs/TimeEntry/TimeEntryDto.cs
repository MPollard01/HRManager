using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.Common;
using HRLeaveManagment.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TimeEntry
{
    public class TimeEntryDto : BaseDto, ITimeEntryDto
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public List<HoursDay> Hours { get; set; }
        public int TotalHours { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Approved { get; set; }
        public string? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
