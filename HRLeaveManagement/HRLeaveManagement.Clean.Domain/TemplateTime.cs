using HRLeaveManagement.Clean.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Clean.Domain
{
    public class TemplateTime : BaseDomainEntity
    {
        public int Hours1 { get; set; }
        public int Hours2 { get; set; }
        public int Hours3 { get; set; }
        public int Hours4 { get; set; }
        public int Hours5 { get; set; }
        public int Hours6 { get; set; }
        public int Hours7 { get; set; }
        public int Total { get; set; }
        public string EmployeeId { get; set; }
    }
}
