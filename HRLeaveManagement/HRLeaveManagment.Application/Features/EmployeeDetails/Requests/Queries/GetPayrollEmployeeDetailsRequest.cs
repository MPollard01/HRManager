using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Queries
{
    public class GetPayrollEmployeeDetailsRequest : IRequest<PayrollEmployeeDetailsDto>
    {
    }
}
