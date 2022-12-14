using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.EmployeeDetails.Validators;
using HRLeaveManagment.Application.Features.EmployeeDetails.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using HRLeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagment.Application.Features.EmployeeDetails.Handlers.Commands
{
    public class CreateEmployeeDetailsCommandHandler : IRequestHandler<CreateEmployeeDetailsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateEmployeeDetailsCommandHandler(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEmployeeDetailsDtoValidator(_userService);
            var validationResult = await validator.ValidateAsync(request.EmployeeDetailsDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Request Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var employeeDetails = _mapper.Map<EmployeeDetail>(request.EmployeeDetailsDto);
                employeeDetails = await _unitOfWork.EmployeeDetailsRepository.Add(employeeDetails);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Details Created Successfully";
                response.Id = employeeDetails.Id;
            }

            return response;
        }
    }
}
