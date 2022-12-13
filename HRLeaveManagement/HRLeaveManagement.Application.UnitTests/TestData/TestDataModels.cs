using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.TestData
{
    internal static class TestDataModels
    {
        internal static List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = "123",
                Firstname = "John",
                Lastname = "Doe",
                Email = "johnD@mail.com"
            },
            new Employee
            {
                Id = "456",
                Firstname = "Jane",
                Lastname = "Doe",
                Email = "janeD@mail.com"
            }
        };

        internal static List<LeaveRequestListDto> leaveRequestListDtos = new List<LeaveRequestListDto>
        {
            new LeaveRequestListDto
            {
                Id = 1,
                StartDate = new DateTime(2022, 11, 5),
                EndDate = new DateTime(2022, 12, 10),
                DateRequested = new DateTime(2022, 10, 1),
                Approved = true,
                Employee = employees[0],
                RequestingEmployeeId = employees[0].Id,
                LeaveType = It.IsAny<LeaveTypeDto>(),
            },
            new LeaveRequestListDto
            {
                Id = 2,
                StartDate = new DateTime(2022, 10, 5),
                EndDate = new DateTime(2022, 10, 10),
                DateRequested = new DateTime(2022, 10, 1),
                Approved = true,
                Employee = employees[1],
                RequestingEmployeeId = employees[1].Id,
                LeaveType = It.IsAny<LeaveTypeDto>(),
            },
            new LeaveRequestListDto
            {
                Id = 3,
                StartDate = new DateTime(2022, 10, 5),
                EndDate = new DateTime(2022, 10, 10),
                DateRequested = new DateTime(2022, 10, 1),
                Approved = null,
                Employee = employees[1],
                RequestingEmployeeId = employees[1].Id,
                LeaveType = It.IsAny<LeaveTypeDto>(),
            },
            new LeaveRequestListDto
            {
                Id = 4,
                StartDate = new DateTime(2022, 12, 10),
                EndDate = new DateTime(2022, 12, 15),
                DateRequested = new DateTime(2022, 10, 1),
                Approved = true,
                Employee = employees[1],
                RequestingEmployeeId = employees[1].Id,
                LeaveType = It.IsAny<LeaveTypeDto>(),
            },
        };

        internal static List<TimeEntryDto> timeEntryDtos = new List<TimeEntryDto>
        {
            new TimeEntryDto
            {
                Id = 1,
                Approved = null,
                TotalHours = 40,
                EmployeeId = employees[0].Id,
                StartWeek = new DateTime(2022, 11, 28),
                EndWeek = new DateTime(2022, 11, 4)
            },
            new TimeEntryDto
            {
                Id = 2,
                Approved = true,
                TotalHours = 29,
                EmployeeId= employees[1].Id,
                StartWeek = new DateTime(2022, 11, 28),
                EndWeek = new DateTime(2022, 12, 4)
            },
            new TimeEntryDto
            {
                Id = 3,
                Approved = null,
                TotalHours = 21,
                EmployeeId = employees[1].Id,
                StartWeek = new DateTime(2022, 11, 7),
                EndWeek = new DateTime(2022, 11, 13)
            },
        };

        internal static List<LeaveRequestVM> leaveRequestVMs = new List<LeaveRequestVM>
        {
            new LeaveRequestVM
            {
                Id = 4,
                StartDate = new DateTime(2022, 12, 10),
                EndDate = new DateTime(2022, 12, 15),
                DateRequested = new DateTime(2022, 10, 1),
                Approved = true,
                Employee = new EmployeeVM
                {
                    Id = "456",
                    Firstname = "Jane",
                    Lastname = "Doe",
                    Email = "janeD@mail.com"
                },
                Cancelled = false,
                LeaveType = It.IsAny<LeaveTypeVM>(),
            }
        };
    }
}
