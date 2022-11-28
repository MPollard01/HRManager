using AutoMapper;
using HRLeaveManagment.Application.DTOs.TimeEntry;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Handlers.Queries
{
    public class GetTimeEntryDetailRequestHandler : IRequestHandler<GetTimeEntryDetailRequest, TimeEntryDto>
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetTimeEntryDetailRequestHandler(ITimeEntryRepository timeEntryRepository, 
            IMapper mapper, IUserService userService)
        {
            _timeEntryRepository = timeEntryRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<TimeEntryDto> Handle(GetTimeEntryDetailRequest request, CancellationToken cancellationToken)
        {
            var timeEntry = _mapper.Map<TimeEntryDto>(await _timeEntryRepository.Get(request.Id));
            timeEntry.Employee = await _userService.GetEmployee(timeEntry.EmployeeId);
            return timeEntry;
        }
    }
}
