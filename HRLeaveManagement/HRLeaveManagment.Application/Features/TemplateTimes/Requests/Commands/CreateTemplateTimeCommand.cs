using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands
{
    public class CreateTemplateTimeCommand : IRequest<BaseCommandResponse>
    {
        public CreateTemplateTimeDto TemplateTimeDto { get; set; }
    }
}
