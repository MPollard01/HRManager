using AutoMapper;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.Payroll;
using HRLeaveManagment.Application.Features.Payrolls.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagment.Application.Features.Payrolls.Handlers.Queries
{
    public class GetPayrollListRequestHandler : IRequestHandler<GetPayrollListRequest, List<PayrollDto>>
    {
        private readonly IPayrollRepository _payrollRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetPayrollListRequestHandler(IPayrollRepository payrollRepo,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _payrollRepo = payrollRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<PayrollDto>> Handle(GetPayrollListRequest request, CancellationToken cancellationToken)
        {
            var payrolls = new List<PayrollDto>();

            if (request.IsUserLoggedIn)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

                var payrollList = await _payrollRepo.GetPayrollsByEmployeeId(userId);
                var employee = await _userService.GetEmployee(userId);
                payrolls = _mapper.Map<List<PayrollDto>>(payrollList);

                foreach(var payroll in payrolls)
                    payroll.Employee = employee;
            }
            else
            {
                var payrollList = await _payrollRepo.GetAll();
                payrolls = _mapper.Map<List<PayrollDto>>(payrollList);

                foreach(var payroll in payrolls)
                    payroll.Employee = await _userService.GetEmployee(payroll.EmployeeId);
            }

            return payrolls;
        }
    }
}
