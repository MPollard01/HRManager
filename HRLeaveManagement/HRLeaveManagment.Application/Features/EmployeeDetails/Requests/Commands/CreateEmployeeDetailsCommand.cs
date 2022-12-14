using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using HRLeaveManagment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Commands
{
    public class CreateEmployeeDetailsCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmployeeDetailsDto EmployeeDetailsDto { get; set; }
    }
}
