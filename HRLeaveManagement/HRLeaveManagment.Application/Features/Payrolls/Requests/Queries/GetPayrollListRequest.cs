using HRLeaveManagment.Application.DTOs.Payroll;
using MediatR;

namespace HRLeaveManagment.Application.Features.Payrolls.Requests.Queries
{
    public class GetPayrollListRequest : IRequest<List<PayrollDto>>
    {
        public bool IsUserLoggedIn { get; set; }
    }
}
