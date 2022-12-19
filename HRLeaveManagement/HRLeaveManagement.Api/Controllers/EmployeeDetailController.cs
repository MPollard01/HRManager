using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Commands;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Queries;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDetailsDto>> Get()
        {
            var employeeDetails = await _mediator.Send(new GetEmployeeDetailsRequest());
            return Ok(employeeDetails);
        }

        [HttpGet("payroll")]
        public async Task<ActionResult<PayrollEmployeeDetailsDto>> GetPayroll()
        {
            var employeeDetails = await _mediator.Send(new GetPayrollEmployeeDetailsRequest());
            return Ok(employeeDetails);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeDetailsDto dto)
        {
            var command = new CreateEmployeeDetailsCommand { EmployeeDetailsDto = dto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Put([FromBody] UpdateEmployeeDetailsDto dto)
        {
            var command = new UpdateEmployeeDetailsCommand { EmployeeDetailsDto = dto };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
