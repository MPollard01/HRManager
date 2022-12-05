using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagment.Application.DTOs.LeaveType;
using HRLeaveManagment.Application.Exceptions;
using HRLeaveManagment.Application.Features.LeaveTypes.Handlers.Commands;
using HRLeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Profiles;
using HRLeaveManagment.Application.Responses;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);

            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test Dto"
            };
        }

        [Fact]
        public async Task Handle_WhenCalled_AddsALeaveTypeInTheRepository()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();
            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenInvalidDto_ReturnsFailedResponse()
        {
            _leaveTypeDto.DefaultDays = -1;

            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

            leaveTypes.Count.ShouldBe(2);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBe(false);
        }
    }
}
