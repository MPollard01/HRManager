using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.TimeEntry;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Handlers.Queries
{
    public class GetTimeEntryListRequestHandler : IRequestHandler<GetTimeEntryListRequest, List<TimeEntryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetTimeEntryListRequestHandler(IUnitOfWork unitOfWork,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<TimeEntryDto>> Handle(GetTimeEntryListRequest request, CancellationToken cancellationToken)
        {
            var timeEntries = new List<TimeEntry>();
            var entries = new List<TimeEntryDto>();

            if (request.IsUserLoggedIn)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

                timeEntries = await _unitOfWork.TimeEntryRepository.GetEmployeeTimeEntrys(userId);
                var employee = await _userService.GetEmployee(userId);
                entries = _mapper.Map<List<TimeEntryDto>>(timeEntries);

                foreach(var entry in entries)
                    entry.Employee = employee;
            }
            else
            {
                timeEntries = await _unitOfWork.TimeEntryRepository.GetTimeEntriesWithDetails();
                entries = _mapper.Map<List<TimeEntryDto>>(timeEntries);

                foreach(var entry in entries)
                    entry.Employee = await _userService.GetEmployee(entry.EmployeeId);
            }

            return entries;
        }
    }
}
