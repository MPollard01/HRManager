using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.DTOs.TimeEntry.Validators;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.TimeEntries.Handlers.Commands
{
    public class CreateTimeEntryCommandHandler : IRequestHandler<CreateTimeEntryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateTimeEntryCommandHandler(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTimeEntryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TimeEntryDto);
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
                var timeEntry = _mapper.Map<TimeEntry>(request.TimeEntryDto);
                timeEntry.EmployeeId = userId;

                foreach (var hours in timeEntry.Hours)
                {
                    hours.TimeEntryId = timeEntry.Id;
                    await _unitOfWork.HoursDayRepository.Add(hours);
                }

                timeEntry = await _unitOfWork.TimeEntryRepository.Add(timeEntry);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successfull";
                response.Id = timeEntry.Id;
            }

            return response;
        }
    }
}
