using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.LeaveRequest.Valdiators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(lr => lr.StartDate)
                .LessThan(lr => lr.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(lr => lr.EndDate)
                .GreaterThan(lr => lr.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(lr => lr.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                    return leaveTypeExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
