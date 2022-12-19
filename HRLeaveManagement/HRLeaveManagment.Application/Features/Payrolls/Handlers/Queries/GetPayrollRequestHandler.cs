using AutoMapper;
using HRLeaveManagment.Application.DTOs.Payroll;
using HRLeaveManagment.Application.Features.Payrolls.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagment.Application.Features.Payrolls.Handlers.Queries
{
    public class GetPayrollRequestHandler : IRequestHandler<GetPayrollRequest, PayrollDto>
    {
        private readonly IPayrollRepository _payrollRepo;
        private readonly IMapper _mapper;

        public GetPayrollRequestHandler(IPayrollRepository payrollRepo, IMapper mapper)
        {
            _payrollRepo = payrollRepo;
            _mapper = mapper;
        }

        public async Task<PayrollDto> Handle(GetPayrollRequest request, CancellationToken cancellationToken)
        {
            var payroll = await _payrollRepo.Get(request.Id);
            return _mapper.Map<PayrollDto>(payroll);
        }
    }
}
