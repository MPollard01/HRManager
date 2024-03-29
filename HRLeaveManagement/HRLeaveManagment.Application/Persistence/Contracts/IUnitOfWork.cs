﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveAllocationRepository LeaveAllocationRepository { get; }
        ILeaveRequestRepository LeaveRequestRepository { get; }
        ILeaveTypeRepository LeaveTypeRepository { get; }
        ITimeEntryRepository TimeEntryRepository { get; }
        IHoursDayRepository HoursDayRepository { get; }
        ITemplateTimeRepository TemplateTimeRepository { get; }
        IEmployeeDetailsRepository EmployeeDetailsRepository { get; }
        IPayrollRepository PayrollRepository { get; }
        Task Save();
    }
}
