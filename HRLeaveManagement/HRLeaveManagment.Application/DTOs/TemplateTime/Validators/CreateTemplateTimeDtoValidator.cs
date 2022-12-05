using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TemplateTime.Validators
{
    public class CreateTemplateTimeDtoValidator : AbstractValidator<CreateTemplateTimeDto>
    {
        public CreateTemplateTimeDtoValidator()
        {
            Include(new ITemplateTimeDtoValidator());
        }
    }
}
