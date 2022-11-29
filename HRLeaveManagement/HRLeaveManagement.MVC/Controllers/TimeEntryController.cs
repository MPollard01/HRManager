using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers
{
    [Authorize]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryService _timeEntryService;
        private readonly IMapper _mapper;

        public TimeEntryController(ITimeEntryService timeEntryService, IMapper mapper)
        {
            _timeEntryService = timeEntryService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(DateTime? date)
        {
            var entry = await _timeEntryService.GetTimeEntryByDate(date ?? DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek)+1));
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTimeEntryVM timeEntry)
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

            var model = await _timeEntryService.GetTimeEntries();
            return View(model);
        }
    }
}
