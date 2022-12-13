using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using HRLeaveManagment.Application.Constants;

namespace HRLeaveManagement.MVC.Services
{
    public class DashboardService : BaseHttpService, IDashboardService
    {
        private readonly IMapper _mapper;
        public DashboardService(IClient client, 
            ILocalStorageService localStorageService, 
            IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<DashboardVM> GetDashboardDetails()
        {
            AddBearerToken();
            var employees = await _client.EmployeeAllAsync();
            var requests = await _client.LeaveRequestAllAsync(false);
            var entries = await _client.TimeEntryAllAsync(false);

            var approvedRequests = requests.Where(r => r.Approved == true &&
                                                  r.EndDate.Year == DateTime.Now.Year);

            var daysAbscent = new int[12];

            foreach(var req in approvedRequests)
            {
                var numDays = req.EndDate.Day - req.StartDate.Day;
                var startMonth = req.StartDate.Month;
                var endMonth = req.EndDate.Month;
                var monthDiff = endMonth - startMonth;

                if (monthDiff != 0)
                {
                    for(int i = startMonth; i <= endMonth; i++)
                    {
                        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, i);

                        if (i == startMonth)
                            daysAbscent[i - 1] += daysInMonth - req.StartDate.Day;
                        else if (i == endMonth)
                            daysAbscent[i - 1] += req.EndDate.Day;
                        else
                            daysAbscent[i - 1] += daysInMonth;
                    }
                }
                else
                {
                    daysAbscent[startMonth - 1] += numDays;
                }
            }

            var requestsVM = requests.Where(r => r.StartDate <= DateTime.Today && 
                                            r.EndDate >= DateTime.Today && r.Approved == true);

            var model = new DashboardVM
            {
                EmployeeCount = employees.Count,
                RequestCount = requests.Count(r => r.Approved == null),
                TimeEntryApprovalCount = entries.Count(e => e.Approved == null),
                DaysAbscent = daysAbscent,
                Requests = _mapper.Map<List<LeaveRequestVM>>(requestsVM)
            };

            return model;
        }

        public async Task<EmployeeDashboardVM> GetEmployeeDashboardDetails()
        {
            AddBearerToken();
            var requests = await _client.LeaveRequestAllAsync(true);
            var entries = await _client.TimeEntryAllAsync(true);
            var employee = await _client.SelfAsync();

            var monthlyHours = new int[12];

            foreach(var entry in entries)
            {
                monthlyHours[entry.StartWeek.Month - 1] += entry.TotalHours;
            }

            var model = new EmployeeDashboardVM
            {
                RequestCount = requests.Count(r => r.Approved == true),
                TimeEntryApprovalCount = entries.Count(e => e.Approved == true),
                MonthlyHours = monthlyHours,
                Requests = _mapper.Map<List<LeaveRequestVM>>(requests),
                Employee = _mapper.Map<EmployeeVM>(employee)
            };

            return model;
        }
    }
}
