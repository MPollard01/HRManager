using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails.Validators
{
    public class CreateEmployeeDetailsDtoValidator : AbstractValidator<CreateEmployeeDetailsDto>
    {
        private readonly IUserService _userService;

        public CreateEmployeeDetailsDtoValidator(IUserService userService)
        {
            _userService = userService;
            Include(new EmployeeDetailsDtoValidator(_userService));
        }
    }
}
