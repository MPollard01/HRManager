using HRLeaveManagement.MVC.Models;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDetailsVM>> GetEmployeeDetails();
    }
}
