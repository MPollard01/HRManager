using AutoMapper;
using HRLeaveManagment.Application.DTOs.TemplateTime.Validators;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Commands
{
    public class UpdateTemplateTimeCommandHandler : IRequestHandler<UpdateTemplateTimeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTemplateTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTemplateTimeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTemplateTimeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TemplateTimeDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var template = await _unitOfWork.TemplateTimeRepository.Get(request.TemplateTimeDto.Id);

            if(template == null)
                throw new NotFoundException(nameof(template), request.TemplateTimeDto.Id);

            _mapper.Map(request.TemplateTimeDto, template);

            template.Total = template.Hours1 + template.Hours2 + template.Hours3 +
                    template.Hours4 + template.Hours5 + template.Hours6 + template.Hours7;

            await _unitOfWork.TemplateTimeRepository.Update(template);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
