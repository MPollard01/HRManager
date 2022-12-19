using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.Persistence.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HRLeaveManagementDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ILeaveAllocationRepository _leaveAllocationRepository;
        private ILeaveTypeRepository _leaveTypeRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private ITimeEntryRepository _timeEntryRepository;
        private IHoursDayRepository _hoursDayRepository;
        private ITemplateTimeRepository _templateTimeRepository;
        private IEmployeeDetailsRepository _employeeDetailsRepository;
        private IPayrollRepository _payrollRepository;


        public UnitOfWork(HRLeaveManagementDbContext context, 
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public ILeaveAllocationRepository LeaveAllocationRepository =>
            _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);
        public ILeaveTypeRepository LeaveTypeRepository =>
            _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        public ILeaveRequestRepository LeaveRequestRepository =>
            _leaveRequestRepository ??= new LeaveRequestRepository(_context);
        public ITimeEntryRepository TimeEntryRepository =>
            _timeEntryRepository ??= new TimeEntryRepository(_context);
        public IHoursDayRepository HoursDayRepository =>
            _hoursDayRepository ??= new HoursDayRepository(_context);
        public ITemplateTimeRepository TemplateTimeRepository =>
            _templateTimeRepository ??= new TemplateTimeRepository(_context);
        public IEmployeeDetailsRepository EmployeeDetailsRepository =>
            _employeeDetailsRepository ??= new EmployeeDetailsRepository(_context);
        public IPayrollRepository PayrollRepository =>
            _payrollRepository ??= new PayrollRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            await _context.SaveChangesAsync(username);
        }
    }
}
