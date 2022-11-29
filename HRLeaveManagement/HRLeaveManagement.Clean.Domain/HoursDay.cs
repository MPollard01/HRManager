using HRLeaveManagement.Clean.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Clean.Domain
{
    public class HoursDay
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int Hours { get; set; }
        public int TimeEntryId { get; set; }
    }
}
