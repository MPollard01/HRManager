using HRLeaveManagment.Application.Models.Identity;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _userService.GetEmployees());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            return Ok(await _userService.GetEmployee(id));
        }

        [HttpGet("self")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Employee>> GetSelf()
        {
            return Ok(await _userService.GetEmployeeSelf());
        }

        [HttpGet("leave/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<int>> GetEmployeeLeaveDays(string id)
        {
            return Ok(await _userService.GetAllocatedDays(id));
        }
    }
}
