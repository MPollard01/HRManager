using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        private readonly IEmployeeDetailService _employeeDetailService;
        private readonly IPayrollService _payrollService;

        public PayrollController(IEmployeeDetailService employeeDetailService,
            IPayrollService payrollService)
        {
            _employeeDetailService = employeeDetailService;
            _payrollService = payrollService;
        }

        public async Task<ActionResult> Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PaySortParm"] = sortOrder == "Pay" ? "pay_desc" : "Pay";

            var payrollEmployeeVM = await _employeeDetailService.GetPayrollEmployeeDetails();

            var payroll = await _payrollService.GetPayrollList(sortOrder);
            var model = new PayrollEmployeeViewVM 
            { 
                Payrolls = PaginatedList<PayrollVM>.Create(payroll, pageNumber ?? 1, 10), 
                PayrollEmployee = payrollEmployeeVM 
            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Employees(string searchString, string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PaySortParm"] = sortOrder == "Pay" ? "pay_desc" : "Pay";
            ViewData["CurrentFilter"] = searchString;

            var model = await _payrollService.GetPayrollAdminList(searchString, sortOrder, pageNumber);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(CreatePayrollVM payroll)
        {
            try
            {
                var response = await _payrollService.CreatePayroll(payroll);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Employees));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(payroll);
        }
    }
}
