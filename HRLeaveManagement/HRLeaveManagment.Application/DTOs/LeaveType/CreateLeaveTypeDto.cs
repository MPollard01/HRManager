using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.LeaveType
{
    public class CreateLeaveTypeDto : ILeaveTypeDto
    {
        public string Name { get; set; } = null!;
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
