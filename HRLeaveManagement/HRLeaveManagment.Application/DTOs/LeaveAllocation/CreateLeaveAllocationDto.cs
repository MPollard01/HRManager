using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDto : CreateBaseDto
    {
        public int LeaveTypeId { get; set; }
    }
}
