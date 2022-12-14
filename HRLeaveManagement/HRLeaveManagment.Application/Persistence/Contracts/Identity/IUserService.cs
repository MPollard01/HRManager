using HRLeaveManagment.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Persistence.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
        Task<Employee> GetEmployeeSelf();
        Task<bool> EmployeeExists(string userId);
        Task<int> GetAllocatedDays(string userId);
        //Task<int> GetRemainingDays(string userId);
        //Task<int> GetTotalDays(string userId);
    }
}
