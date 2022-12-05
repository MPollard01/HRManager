using AutoMapper;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Queries
{
    public class GetTemplateTimeRequestHandler : IRequestHandler<GetTemplateTimeRequest, TemplateTimeDto>
    {
        private readonly ITemplateTimeRepository _templateTimeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTemplateTimeRequestHandler(ITemplateTimeRepository templateTimeRepository,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _templateTimeRepository = templateTimeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TemplateTimeDto> Handle(GetTemplateTimeRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

            var template = await _templateTimeRepository.GetEmployeeTemplateTime(userId);

            TemplateTimeDto templateTime;
            if(template != null)
                templateTime = _mapper.Map<TemplateTimeDto>(template);
            else 
                templateTime = new TemplateTimeDto();

            return templateTime;
        }
    }
}
