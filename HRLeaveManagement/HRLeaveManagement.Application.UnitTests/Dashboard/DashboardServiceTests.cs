using AutoMapper;
using HRLeaveManagement.Application.UnitTests.TestData;
using HRLeaveManagement.MVC;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using HRLeaveManagement.MVC.Services.Base;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Application.UnitTests.Dashboard
{
    public class DashboardServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IClient> _mockClient;
        private readonly DashboardService _sut;

        public DashboardServiceTests()
        {
            _mockClient = new Mock<IClient>();
            var storageService = new Mock<ILocalStorageService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _sut = new DashboardService(_mockClient.Object, storageService.Object, _mapper);        
        }

        [Fact]
        public async Task GetDashboardDetails_WhenCalled_ReturnsExpectedResult()
        {
            _mockClient.Setup(c => c.EmployeeAllAsync()).ReturnsAsync(TestDataModels.employees);
            _mockClient.Setup(c => c.LeaveRequestAllAsync(false)).ReturnsAsync(TestDataModels.leaveRequestListDtos);
            _mockClient.Setup(c => c.TimeEntryAllAsync(false)).ReturnsAsync(TestDataModels.timeEntryDtos);

            var result = await _sut.GetDashboardDetails();

            var expectedDaysAbscent = new int[12];
            expectedDaysAbscent[9] = 5;
            expectedDaysAbscent[10] = 25;
            expectedDaysAbscent[11] = 15;

            var testRequest = TestDataModels.leaveRequestListDtos[3];
            var expectedRequests = 
                _mapper.Map<List<LeaveRequestVM>>(new List<LeaveRequestListDto> { testRequest });

            result.TimeEntryApprovalCount.ShouldBe(2);
            result.EmployeeCount.ShouldBe(2);
            result.RequestCount.ShouldBe(1);
            result.DaysAbscent.ShouldBe(expectedDaysAbscent);
            result.Requests.Count.ShouldBe(1);
            result.Requests.ShouldBeEquivalentTo(expectedRequests);
            result.Requests[0]?.StartDate.ShouldBe(testRequest.StartDate);
            result.Requests[0]?.EndDate.ShouldBe(testRequest.EndDate);
        }

        [Fact]
        public async Task GetEmployeeDashboardDetails_WhenCalled_ReturnsExpectedResult()
        {
            _mockClient.Setup(c => c.LeaveRequestAllAsync(true))
                .ReturnsAsync(TestDataModels.leaveRequestListDtos.GetRange(1,3));
            _mockClient.Setup(c => c.TimeEntryAllAsync(true))
                .ReturnsAsync(TestDataModels.timeEntryDtos.GetRange(1,2));
            _mockClient.Setup(c => c.SelfAsync())
                .ReturnsAsync(TestDataModels.employees[1]);

            var expectedHours = new int[12];
            expectedHours[10] = 50;

            var result = await _sut.GetEmployeeDashboardDetails();

            result.RequestCount.ShouldBe(2);
            result.Requests.Count.ShouldBe(3);
            result.MonthlyHours.ShouldBe(expectedHours);
            result.TimeEntryApprovalCount.ShouldBe(1);
        }
    }
}
