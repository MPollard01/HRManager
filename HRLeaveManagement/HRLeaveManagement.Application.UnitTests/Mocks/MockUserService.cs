using HRLeaveManagment.Application.Models.Identity;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    internal class MockUserService
    {
        public static Mock<IUserService> GetUserService()
        {
            var employee = new Employee
            {
                Id = "123",
                Email = "mail@mail.com",
                Firstname = "John",
                Lastname = "Doe"
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetEmployee(It.IsAny<string>())).ReturnsAsync(employee);

            return mockService;
        }
    }
}
