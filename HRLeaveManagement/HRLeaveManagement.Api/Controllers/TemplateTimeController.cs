using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Queries;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TemplateTimeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TemplateTimeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TemplateTimeDto>> Get()
        {
            var template = await _mediator.Send(new GetTemplateTimeRequest());
            return Ok(template);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTemplateTimeDto templateTime)
        {
            var command = new CreateTemplateTimeCommand { TemplateTimeDto = templateTime };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TemplateTimeDto templateTime)
        {
            var command = new UpdateTemplateTimeCommand { TemplateTimeDto = templateTime };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
