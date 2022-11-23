using HRLeaveManagment.Application.Models.Identity;
using HRLeaveManagment.Application.Persistence.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _userService.GetEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            return Ok(await _userService.GetEmployee(id));
        }
    }
}
