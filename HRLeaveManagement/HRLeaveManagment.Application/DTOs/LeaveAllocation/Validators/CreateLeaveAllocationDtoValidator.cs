﻿using FluentValidation;
using HRLeaveManagment.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(la => la.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                    return leaveTypeExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
