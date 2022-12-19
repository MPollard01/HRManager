using HRLeaveManagment.Application.DTOs.Payroll;
using HRLeaveManagment.Application.Features.Payrolls.Requests.Commands;
using HRLeaveManagment.Application.Features.Payrolls.Requests.Queries;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PayrollDto>>> Get(bool isLoggedInUser = false)
        {
            var payrolls = await _mediator.Send(new GetPayrollListRequest { IsUserLoggedIn = isLoggedInUser });
            return Ok(payrolls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollDto>> Get(int id)
        {
            var payroll = await _mediator.Send(new GetPayrollRequest { Id = id });
            return Ok(payroll);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePayrollDto dto)
        {
            var command = new CreatePayrollCommand { PayrollDto = dto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
