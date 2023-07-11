using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.LeaveAllocation.Validators;
using HRLeaveManagment.Application.Features.LeaveAllocations.Requests;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using HRLeaveManagment.Application.Responses;
using MediatR;

namespace HRLeaveManagment.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateEmployeeLeaveAllocationCommandHandler : IRequestHandler<CreateEmployeeLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public CreateEmployeeLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<BaseCommandResponse> Handle(CreateEmployeeLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEmployeeLeaveAllocationDtoValidator(_userService);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Allocations Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveTypes = await _unitOfWork.LeaveTypeRepository.GetAll();
                var period = DateTime.UtcNow.Year;
                var allocations = new List<LeaveAllocation>();

                foreach(var leaveType in leaveTypes)
                {
                    allocations.Add(new LeaveAllocation
                    {
                        EmployeeId = request.LeaveAllocationDto.EmployeeId,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                    });
                }

                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);

                response.Success = true;
                response.Message = "Allocations Successful";
            }

            return response;
        }
    }
}
