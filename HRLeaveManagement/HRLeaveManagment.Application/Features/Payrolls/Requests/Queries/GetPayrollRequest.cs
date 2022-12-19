using HRLeaveManagment.Application.DTOs.Payroll;
using MediatR;

namespace HRLeaveManagment.Application.Features.Payrolls.Requests.Queries
{
    public class GetPayrollRequest : IRequest<PayrollDto>
    {
        public int Id { get; set; }
    }
}
