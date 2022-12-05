using HRLeaveManagment.Application.Persistence.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockLeaveRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            var mockLeaveRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository();

            mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveRepo.Object);
            mockUow.Setup(r => r.LeaveRequestRepository).Returns(mockLeaveRequestRepo.Object);

            return mockUow;
        }
    }
}
