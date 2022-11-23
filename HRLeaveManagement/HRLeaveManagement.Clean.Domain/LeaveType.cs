using HRLeaveManagement.Clean.Domain.Common;

namespace HRLeaveManagement.Clean.Domain
{
    public class LeaveType : BaseDomainEntity
    {
        public string Name { get; set; } = null!;
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
    }
}