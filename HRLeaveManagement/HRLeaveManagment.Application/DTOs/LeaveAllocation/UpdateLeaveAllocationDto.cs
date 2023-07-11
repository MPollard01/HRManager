using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.LeaveAllocation
{
    public class UpdateLeaveAllocationDto : UpdateBaseDto, ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
