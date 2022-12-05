using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagment.Application.DTOs.LeaveRequest;
using HRLeaveManagment.Application.Features.LeaveRequests.Handlers.Queries;
using HRLeaveManagment.Application.Features.LeaveRequests.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using HRLeaveManagment.Application.Profiles;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveRequestRepository> _mockRepo;
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<IHttpContextAccessor> _mockContextAccessor;

        public GetLeaveRequestListRequestHandlerTests()
        {
            _mockRepo = MockLeaveRequestRepository.GetLeaveRequestRepository();
            _mockService = MockUserService.GetUserService();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockContextAccessor = MockHttpContext.GetHttpContext();
        }

        [Fact]
        public async Task GivenUserIsLoggedIn_Handle_ReturnsListOfUsersRequests()
        {
            var handler = new GetLeaveRequestListRequestHandler(
                _mockRepo.Object, _mapper, _mockContextAccessor.Object, _mockService.Object);

            var result = await handler.Handle(new GetLeaveRequestListRequest { IsLoggedInUser = true }, CancellationToken.None);

            result.ShouldBeOfType<List<LeaveRequestListDto>>();
            result.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GivenUserIsNotLoggedIn_Handle_ReturnsListOfAllUserRequests()
        {
            var handler = new GetLeaveRequestListRequestHandler(
                _mockRepo.Object, _mapper, _mockContextAccessor.Object, _mockService.Object);

            var result = await handler.Handle(new GetLeaveRequestListRequest { IsLoggedInUser = false }, CancellationToken.None);

            result.ShouldBeOfType<List<LeaveRequestListDto>>();
            result.Count.ShouldBe(2);
        }

    }
}
