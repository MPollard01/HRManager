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
            var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            var mockLeaveRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository();
            var mockLeaveAllocationRepo = MockLeaveAllocationRepository.GetLeaveAllocations();
            var mockTemplateTimeRepo = MockTemplateTimeRepository.GetTemplates();

            mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveTypeRepo.Object);
            mockUow.Setup(r => r.LeaveRequestRepository).Returns(mockLeaveRequestRepo.Object);
            mockUow.Setup(r => r.LeaveAllocationRepository).Returns(mockLeaveAllocationRepo.Object);
            mockUow.Setup(r => r.TemplateTimeRepository).Returns(mockTemplateTimeRepo.Object);

            return mockUow;
        }
    }
}
