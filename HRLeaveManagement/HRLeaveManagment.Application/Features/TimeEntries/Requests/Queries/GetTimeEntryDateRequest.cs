using HRLeaveManagment.Application.DTOs.TimeEntry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Requests.Queries
{
    public class GetTimeEntryDateRequest : IRequest<TimeEntryDto>
    {
        public DateTime Date { get; set; }
    }
}
