using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IEmployeeDetailService _employeeDetailService;

        public ProfileController(IEmployeeDetailService employeeDetailService)
        {
            _employeeDetailService = employeeDetailService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _employeeDetailService.GetEmployeeDetails();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeDetailVM model)
        {
            try
            {
                var response = await _employeeDetailService.UpdateEmployeeDetail(id, model);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Profile Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(nameof(Index), model);
        }
    }
}
