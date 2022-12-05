using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TemplateTime.Validators
{
    public class ITemplateTimeDtoValidator : AbstractValidator<ITemplateTimeDto>
    {
        public ITemplateTimeDtoValidator()
        {
            RuleFor(x => x.Hours1)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours2)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours3)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours4)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours5)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours6)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
            RuleFor(x => x.Hours7)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must not be below zero.")
                .LessThanOrEqualTo(16).WithMessage("{PropertyName} must not exceed 16");
        }
    }
}
