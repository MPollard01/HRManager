using HRLeaveManagment.Application.DTOs.LeaveAllocation;
using HRLeaveManagment.Application.Features.LeaveAllocations.Requests;
using HRLeaveManagment.Application.Features.LeaveAllocations.Requests.Commands;
using HRLeaveManagment.Application.Features.LeaveAllocations.Requests.Queries;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveTypes = await _mediator.Send(new GetLeaveAllocationListRequest { IsLoggedInUser = isLoggedInUser});
            return Ok(leaveTypes);
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id});
            return Ok(leaveType);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
        {
            var command = new CreateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("employee")]
        [EndpointName("LeaveAllocationEmployeePOSTAsync")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeLeaveAllocationDto leaveAllocation)
        {
            var command = new CreateEmployeeLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocation)
        {
            var command = new UpdateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }

}
