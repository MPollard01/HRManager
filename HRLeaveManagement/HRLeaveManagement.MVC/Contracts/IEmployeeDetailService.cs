using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IEmployeeDetailService
    {
        Task<EmployeeDetailVM> GetEmployeeDetails();
        Task<PayrollEmployeeVM> GetPayrollEmployeeDetails();
        Task<Response<int>> CreateEmployeeDetail(CreateEmployeeDetailVM employeeDetail);
        Task<Response<int>>UpdateEmployeeDetail(EmployeeDetailVM employeeDetail);
    }
}
