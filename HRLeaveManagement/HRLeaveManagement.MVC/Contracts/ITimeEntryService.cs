using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntryVM>> GetTimeEntries();
        Task<AdminTimeEntryVM> GetTimeEntry(int id);
        Task<TimeEntryVM> GetTimeEntryByDate(DateTime date);
        Task<TimeEntryVM> GetCopyTimeEntryByDate(DateTime date);
        Task<Response<int>> CreateTimeEntry(TimeEntryVM timeEntry);
        Task ApproveTimeEntry(int id, bool approved);
        Task<AdminTimeEntryViewVM> GetAdminTimeEntries(string searchString, string sortOrder, int? pageNumber);
    }
}
