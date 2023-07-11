using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails.Validators
{
    public class CreateEmployeeDetailsDtoValidator : AbstractValidator<CreateEmployeeDetailsDto>
    {

        public CreateEmployeeDetailsDtoValidator()
        {
            RuleFor(e => e.Address1).MinimumLength(2)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.Address2).MinimumLength(2).NotEmpty()
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.Town).MinimumLength(2).NotEmpty()
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.PersonalEmail).EmailAddress()
                .WithMessage("{PropertyName} must be a valid email address");

            RuleFor(e => e.HomePhoneNumber).GreaterThan(9999)
                .WithMessage("{PropertyName} must have at least 5 numbers");
            RuleFor(e => e.MobileNumber).GreaterThan(9999)
                .WithMessage("{PropertyName} must have at least 5 numbers");
            RuleFor(e => e.WorkMobileNumber).GreaterThan(9999)
                .WithMessage("{PropertyName} must have at least 5 numbers");
            RuleFor(e => e.WorkPhoneNumber).GreaterThan(9999)
                .WithMessage("{PropertyName} must have at least 5 numbers");

            RuleFor(e => e.NINumber).MinimumLength(8)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.AccountName).MinimumLength(2)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.BankAddress).MinimumLength(2)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.AccountNumber).GreaterThan(9999)
                .WithMessage("{PropertyName} must have at least 5 numbers");

            RuleFor(e => e.BankName).MinimumLength(2)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.SortCode).GreaterThan(99999)
                .WithMessage("{PropertyName} must have at least 6 numbers");

            RuleFor(e => e.TaxCode).MinimumLength(2)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");

            RuleFor(e => e.PostCode).MinimumLength(5)
                .WithMessage("{PropertyName} must be at least {ComparisonValue} characters long");
        }
    }
}
