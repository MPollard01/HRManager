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
    internal class MockLeaveAllocationRepository
    {
        public static Mock<ILeaveAllocationRepository> GetLeaveAllocations()
        {
            var leaveAllocations = new List<LeaveAllocation>
            {
                new LeaveAllocation
                {
                    Id = 1,
                    NumberOfDays = 8,
                    LeaveType = new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation",
                        DateCreated = DateTime.Now
                    },
                    LeaveTypeId = 1,
                    Period = 22,
                    EmployeeId = "123"
                },
                new LeaveAllocation
                {
                    Id = 2,
                    NumberOfDays = 5,
                    LeaveType = new LeaveType
                    {
                        Id = 2,
                        DefaultDays = 12,
                        Name = "Sick",
                        DateCreated = DateTime.Now
                    },
                    LeaveTypeId = 2,
                    Period = 22,
                    EmployeeId = "123"
                }
            };

            var mockRepo = new Mock<ILeaveAllocationRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveAllocations);
            mockRepo.Setup(r => r.GetUserAllocations(
                leaveAllocations[0].EmployeeId, leaveAllocations[0].LeaveTypeId)
            ).ReturnsAsync(leaveAllocations[0]);

            mockRepo.Setup(r => r.Add(It.IsAny<LeaveAllocation>())).ReturnsAsync((LeaveAllocation leaveAllocation) =>
            {
                leaveAllocations.Add(leaveAllocation);
                return leaveAllocation;
            });

            return mockRepo;
        }
    }
}
