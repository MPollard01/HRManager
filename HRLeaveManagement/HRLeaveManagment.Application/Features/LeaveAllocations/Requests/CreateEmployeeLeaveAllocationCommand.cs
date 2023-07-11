using HRLeaveManagment.Application.DTOs.LeaveAllocation;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.LeaveAllocations.Requests
{
    public class CreateEmployeeLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmployeeLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
