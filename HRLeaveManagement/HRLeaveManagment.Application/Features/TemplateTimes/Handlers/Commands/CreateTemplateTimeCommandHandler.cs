using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.TemplateTime.Validators;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Commands
{
    public class CreateTemplateTimeCommandHandler : IRequestHandler<CreateTemplateTimeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateTemplateTimeCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateTemplateTimeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTemplateTimeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TemplateTimeDto);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var template = _mapper.Map<TemplateTime>(request.TemplateTimeDto);
                template.EmployeeId = userId;
                template.Total = template.Hours1 + template.Hours2 + template.Hours3 +
                    template.Hours4 + template.Hours5 + template.Hours6 + template.Hours7;

                template = await _unitOfWork.TemplateTimeRepository.Add(template);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successfull";
                response.Id = template.Id;
            }

            return response;
        }
    }
}
