using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
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

        public async Task<List<EmployeeDetailsVM>> GetEmployeeDetails()
        {
            AddBearerToken();
            var employeeDetails = new List<EmployeeDetailsVM>();

            var employees = await _client.EmployeeAllAsync();    
            var leaveRequests = await _client.LeaveRequestAllAsync(false);
           
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
                            .Count(q => q.Approved == null) > 0
                });
            }

            return employeeDetails;
        }
    }
}
