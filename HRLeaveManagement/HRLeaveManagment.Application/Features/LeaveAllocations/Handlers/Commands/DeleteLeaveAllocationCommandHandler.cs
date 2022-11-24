﻿using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.LeaveAllocations.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.Get(request.Id)
                ?? throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            await _leaveAllocationRepository.Delete(leaveAllocation);
            return Unit.Value;
        }
    }
}