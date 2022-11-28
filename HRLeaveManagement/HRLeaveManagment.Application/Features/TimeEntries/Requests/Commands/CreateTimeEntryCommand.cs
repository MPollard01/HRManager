using HRLeaveManagment.Application.DTOs.TimeEntry;
using HRLeaveManagment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Requests.Commands
{
    public class CreateTimeEntryCommand : IRequest<BaseCommandResponse>
    {
        public CreateTimeEntryDto TimeEntryDto { get; set; }
    }
}
