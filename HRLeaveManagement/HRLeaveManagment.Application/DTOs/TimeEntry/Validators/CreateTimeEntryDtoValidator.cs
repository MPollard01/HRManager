using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TimeEntry.Validators
{
    public class CreateTimeEntryDtoValidator : AbstractValidator<CreateTimeEntryDto>
    {
        public CreateTimeEntryDtoValidator()
        {
            Include(new ITimeEntryDtoValidator());
        }
    }
}
