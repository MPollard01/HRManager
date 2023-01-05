using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.Payroll.Validators;
using HRLeaveManagment.Application.Features.Payrolls.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.Payrolls.Handlers.Commands
{
    public class CreatePayrollCommandHandler : IRequestHandler<CreatePayrollCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreatePayrollCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePayrollCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePayrollDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PayrollDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var payroll = _mapper.Map<Payroll>(request.PayrollDto);
                var details = await _uow.EmployeeDetailsRepository
                    .GetEmployeeDetailsByEmployeeId(payroll.EmployeeId);

                var totalHours = await _uow.TimeEntryRepository
                    .GetEmployeeTotalHoursInMonth(payroll.EmployeeId, payroll.PayPeriodEnd);

                var pay = totalHours * details.PayPerHour;

                payroll.NetPay = pay;
                payroll.DateCreated = DateTime.Now;
                payroll = await _uow.PayrollRepository.Add(payroll);
                await _uow.Save();

                response.Success = true;
                response.Message = "Creation Successfull";
                response.Id = payroll.Id;
            }

            return response;
        }
    }
}
