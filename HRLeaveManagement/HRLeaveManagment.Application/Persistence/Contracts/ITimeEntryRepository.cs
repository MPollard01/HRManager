using HRLeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Persistence.Contracts
{
    public interface ITimeEntryRepository : IRepository<TimeEntry>
    {
        Task<List<TimeEntry>> GetTimeEntriesWithDetails();
        Task<TimeEntry?> GetEmployeeTimeEntryByDate(string userId, DateTime date);
        Task<int> GetEmployeeTotalHoursInMonth(string userId, DateTime date);
        Task<List<TimeEntry>> GetEmployeeTimeEntrys(string userId);
        Task ChangeTimeEntryApproval(TimeEntry timeEntry, bool? approvalStatus);
    }
}
