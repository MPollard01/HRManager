using HRLeaveManagement.Identity.Models;
using HRLeaveManagment.Application.Constants;
using HRLeaveManagment.Application.Models.Identity;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, 
            IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

        public async Task<int> GetAllocatedDays(string userId)
        {
            var allocations = await _unitOfWork.LeaveAllocationRepository
                            .GetLeaveAllocationsWithDetails(userId);

            if (allocations == null) return 0;

            return allocations.Sum(q => q.NumberOfDays);
        }

        public async Task<Employee> GetEmployeeSelf()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

            if (userId == null) return new Employee();

            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        //public Task<int> GetRemainingDays(string userId)
        //{

        //}

        //public async Task<int> GetTotalDays(string userId)
        //{
        //    var allocations = await _unitOfWork.LeaveAllocationRepository
        //                    .GetLeaveAllocationsWithDetails(userId);

        //    if (allocations == null) return 0;

        //    return allocations.Sum(q => q.LeaveType.DefaultDays);
        //}
    }
}
