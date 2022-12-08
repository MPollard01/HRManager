using HRLeaveManagment.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TimeEntry
{
    public class ChangeTimeEntryApprovalDto : BaseDto
    {
        public bool Approved { get; set; }
    }
}
