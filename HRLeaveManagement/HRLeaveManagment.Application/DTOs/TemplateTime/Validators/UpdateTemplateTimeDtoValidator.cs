using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TemplateTime.Validators
{
    public class UpdateTemplateTimeDtoValidator : AbstractValidator<TemplateTimeDto>
    {
        public UpdateTemplateTimeDtoValidator()
        {
            Include(new ITemplateTimeDtoValidator());
        }
    }
}
