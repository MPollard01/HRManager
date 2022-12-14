using HRLeaveManagement.Clean.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence
{
    public class HRLeaveManagementDbContext : AuditableDbContext
    {
        public HRLeaveManagementDbContext(DbContextOptions<HRLeaveManagementDbContext> options) : base(options)
        {
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<HoursDay> HoursDay { get; set; }
        public DbSet<TemplateTime> TimeTemplates { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRLeaveManagementDbContext).Assembly);
        }
    }
}