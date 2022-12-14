﻿using AutoMapper;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Handlers.Queries
{
    public class GetEmployeeDetailsRequestHandler : IRequestHandler<GetEmployeeDetailsRequest, EmployeeDetailsDto>
    {
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetEmployeeDetailsRequestHandler(
            IEmployeeDetailsRepository employeeDetailsRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _employeeDetailsRepository = employeeDetailsRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
            var employeeDetails = await _employeeDetailsRepository.GetEmployeeDetailsByEmployeeId(userId);
            return _mapper.Map<EmployeeDetailsDto>(employeeDetails);
        }
    }
}
