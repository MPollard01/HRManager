using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntryVM>> GetTimeEntries();
        Task<TimeEntryVM> GetTimeEntry(int id);
        Task<TimeEntryVM> GetTimeEntryByDate(DateTime date);
        Task<Response<int>> CreateTimeEntry(TimeEntryVM timeEntry);
    }
}
