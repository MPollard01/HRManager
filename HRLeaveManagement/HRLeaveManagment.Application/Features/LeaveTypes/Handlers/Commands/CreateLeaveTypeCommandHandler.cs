using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.LeaveType.Validators;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork leaveTypeRepository, IMapper mapper)
        {
            _uow = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leaveType = await _uow.LeaveTypeRepository.Add(leaveType);
                await _uow.Save();

                response.Success = true;
                response.Message = "Creation Successfull";
                response.Id = leaveType.Id;
            }

            return response;
        }
    }
}
