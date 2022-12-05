using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Create(TimeEntryVM timeEntry)
        {
            if (ModelState.IsValid)
            {
                var response = await _timeEntryService.CreateTimeEntry(timeEntry);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var model = await _timeEntryService.GetTimeEntryByDate(timeEntry.StartWeek);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTemplate(TemplateTimeVM template)
        {
            if (ModelState.IsValid)
            {
                Response<int> response;
                if(template.EmployeeId == null)
                {
                    response = await _templateTimeService.CreateTemplate(template);
                }
                else
                {
                    response = await _templateTimeService.UpdateTemplate(template);
                }

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var model = await _templateTimeService.GetTemplateTime();
            return View(model);
        }
    }
}
