using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.Payroll.Validators
{
    public class CreatePayrollDtoValidator : AbstractValidator<CreatePayrollDto>
    {
        public CreatePayrollDtoValidator()
        {
            Include(new PayrollDtoValidator());
        }
    }
}
