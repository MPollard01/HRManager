using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryService _timeEntryService;
        private readonly ITemplateTimeService _templateTimeService;
        private readonly IMapper _mapper;

        public TimeEntryController(ITimeEntryService timeEntryService, IMapper mapper,
            ITemplateTimeService templateTimeService)
        {
            _timeEntryService = timeEntryService;
            _mapper = mapper;
            _templateTimeService = templateTimeService;
        }

        public async Task<ActionResult> Index(string? date)
        {
            var dateString = date ?? DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek) + 1).ToString("dd-MM-yy");
            var entryDate = DateTime.ParseExact(dateString, "dd-MM-yy", null);
            var entry = await _timeEntryService.GetTimeEntryByDate(entryDate);
            var template = await _templateTimeService.GetTemplateTime();
            var model = new TimeEntryWithTemplateVM { TemplateTime = template, TimeEntry = entry };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TimeEntryWithTemplateVM time)
        {
            if (ModelState.IsValid)
            {
                var response = await _timeEntryService.CreateTimeEntry(time.TimeEntry);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var entry = await _timeEntryService.GetTimeEntryByDate(time.TimeEntry.StartWeek);
            var template = await _templateTimeService.GetTemplateTime();
            var model = new TimeEntryWithTemplateVM { TemplateTime = template, TimeEntry = entry };
            return View(nameof(Index), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveTemplate(TimeEntryWithTemplateVM template)
        {
            if (ModelState.IsValid)
            {
                Response<int> response;
                if(template.TemplateTime.EmployeeId.IsNullOrEmpty())
                    response = await _templateTimeService.CreateTemplate(template.TemplateTime);
                else
                    response = await _templateTimeService.UpdateTemplate(template.TemplateTime);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var model = await _templateTimeService.GetTemplateTime();
            return View(model);
        }

        public async Task<ActionResult> Copy(string date)
        {
            var entryDate = DateTime.ParseExact(date, "dd-MM-yy", null);
            var entry = await _timeEntryService.GetCopyTimeEntryByDate(entryDate);
            var template = await _templateTimeService.GetTemplateTime();
            var model = new TimeEntryWithTemplateVM { TemplateTime = template, TimeEntry = entry };

            return View(nameof(Index), model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Employees(string searchString, string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["RequestedSortParm"] = sortOrder == "Requested" ? "requested_desc" : "Requested";
            ViewData["HoursSortParm"] = sortOrder == "Hours" ? "hours_desc" : "Hours";
            ViewData["CurrentFilter"] = searchString;

            var model = await _timeEntryService.GetAdminTimeEntries(searchString, sortOrder, pageNumber);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _timeEntryService.GetTimeEntry(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveEntry(int id, bool approved)
        {
            try
            {
                await _timeEntryService.ApproveTimeEntry(id, approved);
                return RedirectToAction(nameof(Employees));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Employees));
            }
        }
    }
}
