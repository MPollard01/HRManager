using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.LeaveRequest;
using HRLeaveManagment.Application.Features.LeaveRequests.Handlers.Commands;
using HRLeaveManagment.Application.Features.LeaveRequests.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Infrastructure;
using HRLeaveManagment.Application.Profiles;
using HRLeaveManagment.Application.Responses;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.LeaveRequests.Commands
{
    public class CreateLeaveRequestCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IHttpContextAccessor> _mockContextAccessor;
        private readonly CreateLeaveRequestDto _leaveRequestDto;
        private readonly CreateLeaveRequestCommandHandler _handler;

        public CreateLeaveRequestCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            var mockEmail = new Mock<IEmailSender>();
            _mockContextAccessor = MockHttpContext.GetHttpContext();

            _handler = new CreateLeaveRequestCommandHandler(
               _mockUow.Object, mockEmail.Object, _mockContextAccessor.Object, _mapper);

            _leaveRequestDto = new CreateLeaveRequestDto
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                LeaveTypeId = 1,
                RequestComments = string.Empty,
            };
        }

        [Fact]
        public async Task Handle_WhenCalled_AddsLeaveRequestInTheRepository()
        {
            _mockUow.Setup(r => r.LeaveTypeRepository.Exists(It.IsAny<int>())).ReturnsAsync(true);
            var result = await _handler.Handle(new CreateLeaveRequestCommand { LeaveRequestDto = _leaveRequestDto }, CancellationToken.None);
            var leaveRequests = await _mockUow.Object.LeaveRequestRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();
            leaveRequests.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenLeaveTypeDoesNotExist_ReturnsFailedResponse()
        {
            var result = await _handler.Handle(new CreateLeaveRequestCommand { LeaveRequestDto = _leaveRequestDto }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldContain("Leave Type Id does not exist.");
            result.Success.ShouldBe(false);
        }

        [Fact]
        public async Task Handle_GivenDaysRequestedIsGreaterThanAllocatedNumberOfDays_ReturnsFailedResponse()
        {
            _mockUow.Setup(r => r.LeaveTypeRepository.Exists(It.IsAny<int>())).ReturnsAsync(true);
            _leaveRequestDto.EndDate = DateTime.Now.AddDays(20);
            var result = await _handler.Handle(new CreateLeaveRequestCommand { LeaveRequestDto = _leaveRequestDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldContain("You do not have enough days for this request.");
            result.Success.ShouldBe(false);
        }

        [Fact]
        public async Task Handle_GivenLeaveTypeAllocationsDoNotExist_ReturnsFailedResponse()
        {
            _mockUow.Setup(r => r.LeaveTypeRepository.Exists(It.IsAny<int>())).ReturnsAsync(true);
            _mockUow.Setup(r => r.LeaveAllocationRepository.GetUserAllocations(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((LeaveAllocation)null);
            var result = await _handler.Handle(new CreateLeaveRequestCommand { LeaveRequestDto = _leaveRequestDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldContain("You do not have any allocations for this leave type.");
            result.Success.ShouldBe(false);
        }
    }
}
