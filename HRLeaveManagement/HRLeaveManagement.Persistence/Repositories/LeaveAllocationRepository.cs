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
    public class LeaveAllocationRepository : Repository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HRLeaveManagementDbContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task AllocateAllToUser(string userId)
        {
            var leaveTypes = await _context.LeaveTypes.ToListAsync();
            var allocations = new List<LeaveAllocation>();

            foreach (var leaveType in leaveTypes)
            {
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = userId,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.UtcNow.Year,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                });
            }

            await AddAllocations(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                                        && q.LeaveTypeId == leaveTypeId
                                                        && q.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(la => la.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(q => q.EmployeeId == userId)
                .Include(la => la.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(la => la.LeaveType)
                .FirstOrDefaultAsync(la => la.Id == id);

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(q =>
                                            q.EmployeeId == userId
                                            && q.LeaveTypeId == leaveTypeId);
        }
    }
}
