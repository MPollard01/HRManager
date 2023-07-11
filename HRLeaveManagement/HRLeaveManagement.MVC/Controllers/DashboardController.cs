using HRLeaveManagement.MVC.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                var model = await _dashboardService.GetDashboardDetails();
                return View("Dashboard", model);
            }
            else
            {
                //var model = await _dashboardService.GetEmployeeDashboardDetails();
                return View("MyDashboard");
            }
        }

        public async Task<ActionResult> EmployeeDashboardPartialView()
        {
            var model = await _dashboardService.GetEmployeeDashboardDetails();
            return PartialView("_EmployeeDashboardPartial", model);
        }
    }
}
