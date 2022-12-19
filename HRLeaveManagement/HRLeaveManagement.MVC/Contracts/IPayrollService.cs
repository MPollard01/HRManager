using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IPayrollService
    {
        Task<List<PayrollVM>> GetPayrollList();
        Task<List<PayrollVM>> GetPayrollAdminList();
        Task<PayrollVM> GetPayroll(int id);
        Task<Response<int>> CreatePayroll(CreatePayrollVM payrollVM);
    }
}
