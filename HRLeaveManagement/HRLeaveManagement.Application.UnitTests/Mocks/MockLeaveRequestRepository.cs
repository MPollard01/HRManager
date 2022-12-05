using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    internal class MockLeaveRequestRepository
    {
        private const string USER_ID = "123";
        public static Mock<ILeaveRequestRepository> GetLeaveRequestRepository()
        {
            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    LeaveType = new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation",
                        DateCreated = DateTime.Now,
                    },
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    RequestComments = string.Empty,
                    Approved = false,
                    Cancelled = false,
                    RequestingEmployeeId = It.IsAny<string>()
                },
                new LeaveRequest
                {
                    Id = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    LeaveType = new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation",
                        DateCreated = DateTime.Now,
                    },
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    RequestComments = string.Empty,
                    Approved = true,
                    Cancelled = false,
                    RequestingEmployeeId = USER_ID
                }
            };

            var mockRepo = new Mock<ILeaveRequestRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveRequests);
            mockRepo.Setup(r => r.GetLeaveRequestsWithDetails(USER_ID)).ReturnsAsync(new List<LeaveRequest> { leaveRequests[1] });
            mockRepo.Setup(r => r.GetLeaveRequestsWithDetails()).ReturnsAsync(leaveRequests);
            mockRepo.Setup(r => r.Add(It.IsAny<LeaveRequest>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                leaveRequests.Add(leaveRequest);
                return leaveRequest;
            });

            return mockRepo;
        }
    }
}
