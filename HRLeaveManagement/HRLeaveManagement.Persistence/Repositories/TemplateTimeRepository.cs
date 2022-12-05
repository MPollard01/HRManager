using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class TemplateTimeRepository : Repository<TemplateTime>, ITemplateTimeRepository
    {
        public TemplateTimeRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task<TemplateTime?> GetEmployeeTemplateTime(string userId)
        {
            return await _context.TimeTemplates
                .Where(t => t.EmployeeId == userId).FirstOrDefaultAsync();
        }
    }
}
