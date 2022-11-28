using FluentValidation;
using HRLeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TimeEntry.Validators
{
    public class ITimeEntryDtoValidator : AbstractValidator<ITimeEntryDto>
    {
        public ITimeEntryDtoValidator()
        {
            RuleForEach(t => t.Hours).SetValidator(new HoursValidator());
        }
    }

    public class HoursValidator : AbstractValidator<HoursDay>
    {
        public HoursValidator()
        {
            RuleFor(x => x.Hours)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
        }
    }
}
