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
    public class HoursDayRepository : Repository<HoursDay>, IHoursDayRepository
    {
        public HoursDayRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task<List<HoursDay>> GetEmployeesHours(int id)
        {
            return await _context.HoursDay.Where(q => q.TimeEntryId == id).ToListAsync();
        }
    }
}
