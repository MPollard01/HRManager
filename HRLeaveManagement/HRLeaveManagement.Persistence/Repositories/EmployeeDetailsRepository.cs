using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class EmployeeDetailsRepository : Repository<EmployeeDetails>, IEmployeeDetailsRepository
    {
        public EmployeeDetailsRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }
    }
}
