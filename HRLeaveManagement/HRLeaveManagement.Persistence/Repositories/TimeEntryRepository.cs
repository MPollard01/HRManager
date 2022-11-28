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
    public class TimeEntryRepository : Repository<TimeEntry>, ITimeEntryRepository
    {
        public TimeEntryRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task<List<TimeEntry>> GetEmployeeTimeEntrys(string userId)
        {
            return await _context.TimeEntries
                .Where(q => q.EmployeeId == userId)
                .Include(q => q.Hours)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesWithDetails()
        {
            return await _context.TimeEntries.Include(q => q.Hours).ToListAsync();
        }
    }
}
