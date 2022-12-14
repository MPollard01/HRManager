using AutoMapper;
using HRLeaveManagment.Application.DTOs.EmployeeDetails.Validators;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Handlers.Commands
{
    public class UpdateEmployeeDetailsCommandHandler : IRequestHandler<UpdateEmployeeDetailsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEmployeeDetailsDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EmployeeDetailsDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var employeeDetails = await _unitOfWork.EmployeeDetailsRepository.Get(request.EmployeeDetailsDto.Id);

            if (employeeDetails == null)
                throw new NotFoundException(nameof(employeeDetails), request.EmployeeDetailsDto.Id);

            _mapper.Map(request.EmployeeDetailsDto, employeeDetails);

            await _unitOfWork.EmployeeDetailsRepository.Update(employeeDetails);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
