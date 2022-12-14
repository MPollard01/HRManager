using AutoMapper;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Handlers.Queries
{
    public class GetPayrollEmployeeDetailsRequestHandler : IRequestHandler<GetPayrollEmployeeDetailsRequest, PayrollEmployeeDetailsDto>
    {
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPayrollEmployeeDetailsRequestHandler(IEmployeeDetailsRepository employeeDetailsRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _employeeDetailsRepository = employeeDetailsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PayrollEmployeeDetailsDto> Handle(GetPayrollEmployeeDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
            var employeeDetails = await _employeeDetailsRepository.GetEmployeeDetailsByEmployeeId(userId);
            return _mapper.Map<PayrollEmployeeDetailsDto>(employeeDetails);
        }
    }
}
