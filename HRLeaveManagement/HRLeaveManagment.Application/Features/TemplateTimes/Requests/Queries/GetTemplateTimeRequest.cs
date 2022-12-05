using HRLeaveManagment.Application.DTOs.TemplateTime;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TemplateTimes.Requests.Queries
{
    public class GetTemplateTimeRequest : IRequest<TemplateTimeDto>
    {
    }
}
