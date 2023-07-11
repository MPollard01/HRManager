using HRLeaveManagment.Application.DTOs.LeaveAllocation;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.LeaveAllocations.Requests.Commands
{
    public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
