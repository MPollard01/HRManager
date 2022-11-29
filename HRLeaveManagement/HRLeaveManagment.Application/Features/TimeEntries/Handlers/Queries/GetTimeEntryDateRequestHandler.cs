using AutoMapper;
using HRLeaveManagment.Application.DTOs.TimeEntry;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HRLeaveManagment.Application.Constants;

namespace HRLeaveManagment.Application.Features.TimeEntries.Handlers.Queries
{
    public class GetTimeEntryDateRequestHandler : IRequestHandler<GetTimeEntryDateRequest, TimeEntryDto>
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTimeEntryDateRequestHandler(ITimeEntryRepository timeEntryRepository,
            IMapper mapper, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _timeEntryRepository = timeEntryRepository;
            _mapper = mapper;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TimeEntryDto> Handle(GetTimeEntryDateRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

            var timeEntry = _mapper.Map<TimeEntryDto>(await _timeEntryRepository.GetEmployeeTimeEntryByDate(userId, request.Date));
            timeEntry.Employee = await _userService.GetEmployee(timeEntry.EmployeeId);
            return timeEntry;
        }
    }
}
