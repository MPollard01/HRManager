using HRLeaveManagement.Clean.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Clean.Domain
{
    public class TimeEntry : BaseDomainEntity
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public List<HoursDay> Hours { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Approved { get; set; }
        public string? EmployeeId { get; set; }
    }
}
