using HRLeaveManagement.MVC.Contracts;
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

        public async Task<ActionResult> Index()
        {
            var payrollEmployeeVM = await _employeeDetailService.GetPayrollEmployeeDetails();
            var payroll = await _payrollService.GetPayrollList();
            var model = new PayrollEmployeeViewVM 
            { 
                Payrolls = payroll, 
                PayrollEmployee = payrollEmployeeVM 
            };
            return View(model);
        }

        [Authorize("Administrator")]
        public async Task<ActionResult> Employees()
        {
            var model = await _payrollService.GetPayrollAdminList();
            return View(model);
        }

        [Authorize("Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Administrator")]
        public async Task<ActionResult> Create(CreatePayrollVM payroll)
        {
            try
            {
                var response = await _payrollService.CreatePayroll(payroll);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
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
