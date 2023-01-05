using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using HRLeaveManagment.Application.Models.Identity;

namespace HRLeaveManagement.MVC.Services
{
    public class EmployeeService : BaseHttpService, IEmployeeService
    {
        private readonly IMapper _mapper;
        public EmployeeService(IClient client, ILocalStorageService localStorageService,
            IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedList<EmployeeDetailsVM>> GetEmployeeDetails(string searchString, string sortOrder, int? pageNumber)
        {
            AddBearerToken();
            var employeeDetails = new List<EmployeeDetailsVM>();

            var employees = await _client.EmployeeAllAsync();    
            var leaveRequests = await _client.LeaveRequestAllAsync(false);
            var timeEnties = await _client.TimeEntryAllAsync(false);

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.Lastname.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || e.Firstname.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "Name":
                    employees = employees.OrderBy(e => e.Lastname).ToList();
                    break;
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Lastname).ToList();
                    break;
                default:
                    break;
            }

            foreach (var employee in employees)
            {         
                employeeDetails.Add(new EmployeeDetailsVM
                {
                    Employee = new EmployeeVM
                    {
                        Id = employee.Id,
                        Email = employee.Email,
                        Firstname = employee.Firstname,
                        Lastname = employee.Lastname
                    },
                    NumberOfDays = await _client.LeaveAsync(employee.Id),
                    HasRequests = leaveRequests
                            .Where(e => e.RequestingEmployeeId == employee.Id)
                            .Count(q => q.Approved == null),
                    TimeEntries = timeEnties
                            .Where(e => e.EmployeeId == employee.Id)
                            .Count(e => e.Approved == null)
                });
            }

            return PaginatedList<EmployeeDetailsVM>.Create(employeeDetails, pageNumber ?? 1, 10);
        }
    }
}
