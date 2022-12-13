using HRLeaveManagement.MVC.Models;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetDashboardDetails();
        Task<EmployeeDashboardVM> GetEmployeeDashboardDetails();
    }
}
