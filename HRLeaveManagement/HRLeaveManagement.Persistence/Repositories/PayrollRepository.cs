using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class PayrollRepository : Repository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task<List<Payroll>> GetPayrollsByEmployeeId(string employeeId)
        {
            return await _context.Payroll.Where(p => p.Equals(employeeId)).ToListAsync();
        }
    }
}
