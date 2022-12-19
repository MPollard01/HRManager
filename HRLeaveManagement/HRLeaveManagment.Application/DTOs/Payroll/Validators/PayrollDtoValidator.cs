using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.Payroll.Validators
{
    public class PayrollDtoValidator : AbstractValidator<IPayrollDto>
    {
        public PayrollDtoValidator()
        {    
            RuleFor(p => p.PeriodNumber).GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(p => p.NetPay).GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
