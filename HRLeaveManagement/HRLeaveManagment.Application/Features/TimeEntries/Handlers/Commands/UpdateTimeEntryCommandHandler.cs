using AutoMapper;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.TimeEntries.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagment.Application.Features.TimeEntries.Handlers.Commands
{
    public class UpdateTimeEntryCommandHandler : IRequestHandler<UpdateTimeEntryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTimeEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var timeEntry = await _unitOfWork.TimeEntryRepository.Get(request.Id);

            if(timeEntry == null) 
                throw new NotFoundException(nameof(timeEntry), request.Id);

            if(request.ChangeTimeEntryApprovalDto != null)
            {
                await _unitOfWork.TimeEntryRepository.ChangeTimeEntryApproval(timeEntry, request.ChangeTimeEntryApprovalDto.Approved);

                if (request.ChangeTimeEntryApprovalDto.Approved)
                {
                    // calculate pay

                    await _unitOfWork.TimeEntryRepository.Update(timeEntry);
                }

                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}
