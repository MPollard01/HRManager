using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails.Validators
{
    public class EmployeeDetailsDtoValidator : AbstractValidator<IEmployeeDetailsDto>
    {
        private readonly IUserService _userService;

        public EmployeeDetailsDtoValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(e => e.EmployeeId)
                .MustAsync(async (id, token) => await _userService.EmployeeExists(id))
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
