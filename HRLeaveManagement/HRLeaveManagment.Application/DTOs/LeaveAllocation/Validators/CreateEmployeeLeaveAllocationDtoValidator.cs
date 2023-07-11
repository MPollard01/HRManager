using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;

namespace HRLeaveManagment.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateEmployeeLeaveAllocationDtoValidator : AbstractValidator<CreateEmployeeLeaveAllocationDto>
    {
        private readonly IUserService _userService;

        public CreateEmployeeLeaveAllocationDtoValidator(IUserService userService)
        {
            _userService = userService;
            
            RuleFor(la => la.EmployeeId)
                .MustAsync(async (id, token) =>
                {
                    var employeeExists = await _userService.EmployeeExists(id);
                    return employeeExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
