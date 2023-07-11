using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.TimeEntry
{
    public class CreateTimeEntryDto : CreateBaseDto, ITimeEntryDto
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public List<HoursDay> Hours { get; set; }
        public int TotalHours { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
