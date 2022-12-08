using HRLeaveManagment.Application.DTOs.TimeEntry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Requests.Commands
{
    public class UpdateTimeEntryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public ChangeTimeEntryApprovalDto? ChangeTimeEntryApprovalDto { get; set; }
    }
}
