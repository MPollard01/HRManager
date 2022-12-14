

using HRLeaveManagement.Clean.Domain;

namespace HRLeaveManagment.Application.Persistence.Contracts
{
    public interface IEmployeeDetailsRepository : IRepository<EmployeeDetail>
    {
        Task<EmployeeDetail?> GetEmployeeDetailsByEmployeeId(string employeeId);
    }
}
