using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class EmployeeDetailsRepository : Repository<EmployeeDetail>, IEmployeeDetailsRepository
    {
        public EmployeeDetailsRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task<EmployeeDetail?> GetEmployeeDetailsByEmployeeId(string employeeId)
        {
            return await _context.EmployeeDetails
                .Where(e => e.EmployeeId == employeeId)
                .FirstOrDefaultAsync();
        }
    }
}
