using HRLeaveManagment.Application.DTOs.Payroll;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.Payrolls.Requests.Commands
{
    public class CreatePayrollCommand : IRequest<BaseCommandResponse>
    {
        public CreatePayrollDto PayrollDto{ get; set; }
    }
}
