using HRLeaveManagement.Clean.Domain;

namespace HRLeaveManagment.Application.Persistence.Contracts
{
    public interface IPayrollRepository : IRepository<Payroll>
    {
        Task<List<Payroll>> GetPayrollsByEmployeeId(string employeeId);
    }
}
