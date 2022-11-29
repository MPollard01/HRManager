﻿using HRLeaveManagment.Application.DTOs.TimeEntry;
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
        public async Task<ActionResult<TimeEntryDto>> GetByDate(string date)
        {
            var timeEntry = await _mediator.Send(new GetTimeEntryDateRequest { Date = DateTime.Parse(date) });
            return Ok(timeEntry);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTimeEntryDto timeEntry)
        {
            var command = new CreateTimeEntryCommand { TimeEntryDto = timeEntry };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
