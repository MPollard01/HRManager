using HRLeaveManagment.Application.DTOs.TimeEntry;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Commands;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Queries;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeEntryDto>>> Get(bool isLoggedInUser = false)
        {
            var timeEntries = await _mediator.Send(new GetTimeEntryListRequest { IsUserLoggedIn = isLoggedInUser });
            return Ok(timeEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryDto>> Get(int id)
        {
            var timeEntry = await _mediator.Send(new GetTimeEntryDetailRequest { Id = id });
            return Ok(timeEntry);
        }

        [HttpGet("byDate")]
        public async Task<ActionResult<TimeEntryDto>> GetByDate(string? date)
        {
            var dateString = date ?? DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek) + 1).ToString("dd-MM-yy");
            var entryDate = DateTime.ParseExact(dateString, "dd-MM-yy", null);
            var timeEntry = await _mediator.Send(new GetTimeEntryDateRequest { Date = entryDate });

            return Ok(timeEntry);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTimeEntryDto timeEntry)
        {
            var command = new CreateTimeEntryCommand { TimeEntryDto = timeEntry };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("changetimeapproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeTimeEntryApprovalDto approval)
        {
            var command = new UpdateTimeEntryCommand { Id = id, ChangeTimeEntryApprovalDto = approval };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
