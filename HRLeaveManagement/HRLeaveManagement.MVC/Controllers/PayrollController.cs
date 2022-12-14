using HRLeaveManagement.MVC.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        private readonly IEmployeeDetailService _employeeDetailService;

        public PayrollController(IEmployeeDetailService employeeDetailService)
        {
            _employeeDetailService = employeeDetailService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _employeeDetailService.GetPayrollEmployeeDetails();
            return View(model);
        }
    }
}
