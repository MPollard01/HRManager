using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IEmployeeService
    {
        Task<PaginatedList<EmployeeDetailsVM>> GetEmployeeDetails(string searchString, string sortOrder, int? pageNumber);
    }
}
