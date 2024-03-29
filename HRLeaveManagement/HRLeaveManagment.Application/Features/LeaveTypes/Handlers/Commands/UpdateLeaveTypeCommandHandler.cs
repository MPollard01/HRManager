﻿using AutoMapper;
using HRLeaveManagment.Application.DTOs.LeaveType.Validators;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveTypeDto.Id);

            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.LeaveTypeDto.Id);

            _mapper.Map(request.LeaveTypeDto, leaveType);
            leaveType.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.LeaveTypeRepository.Update(leaveType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
