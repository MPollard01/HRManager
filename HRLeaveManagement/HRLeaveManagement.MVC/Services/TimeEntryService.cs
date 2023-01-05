using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class TimeEntryService : BaseHttpService, ITimeEntryService
    {
        private readonly IMapper _mapper;
        public TimeEntryService(IClient client, 
            ILocalStorageService localStorageService, IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateTimeEntry(TimeEntryVM timeEntry)
        {
            try
            {
                var response = new Response<int>();
                var createTimeEntry = _mapper.Map<CreateTimeEntryDto>(timeEntry);
                AddBearerToken();
                var apiResponse = await _client.TimeEntryPOSTAsync(createTimeEntry);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                    response.Message = apiResponse.Message;
                }
                else
                {
                    response.ValidationErrors =
                        string.Join(Environment.NewLine, apiResponse.Errors);
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<List<TimeEntryVM>> GetTimeEntries()
        {
            AddBearerToken();
            var timeEntries = await _client.TimeEntryAllAsync(true);
            return _mapper.Map<List<TimeEntryVM>>(timeEntries);
        }

        public async Task<AdminTimeEntryVM> GetTimeEntry(int id)
        {
            AddBearerToken();
            var timeEntries = await _client.TimeEntryGETAsync(id);
            return _mapper.Map<AdminTimeEntryVM>(timeEntries);
        }

        public async Task<TimeEntryVM> GetTimeEntryByDate(DateTime date)
        {
            AddBearerToken();
            var timeEntry = await _client.ByDateAsync(date.ToString("dd-MM-yy"));

            if (timeEntry.Hours == null)
            {
                var entry = new TimeEntryVM
                {
                    StartWeek = date,
                    EndWeek = date.AddDays(6),
                    Hours = new List<HoursDay>()
                    {
                        new HoursDay(),
                        new HoursDay(),
                        new HoursDay(),
                        new HoursDay(),
                        new HoursDay(),
                        new HoursDay(),
                        new HoursDay()
                    }
                };
                
                return entry;
            }

            return _mapper.Map<TimeEntryVM>(timeEntry);
        }

        public async Task<TimeEntryVM> GetCopyTimeEntryByDate(DateTime date)
        {
            var timeEntry = await GetTimeEntryByDate(date);
            var previous = await GetTimeEntryByDate(date.AddDays(-7));

            for(int i = 0; i < timeEntry.Hours.Count; i++)
                timeEntry.Hours[i].Hours = previous.Hours[i].Hours;

            return timeEntry;
        }

        public async Task ApproveTimeEntry(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var request = new ChangeTimeEntryApprovalDto { Approved = approved, Id = id };
                await _client.ChangetimeapprovalAsync(id, request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AdminTimeEntryViewVM> GetAdminTimeEntries(string searchString, string sortOrder, int? pageNumber)
        {
            AddBearerToken();
            var entries = await _client.TimeEntryAllAsync(isLoggedInUser: false);
            
            if(!string.IsNullOrEmpty(searchString))
            {
                entries = entries.Where(e => e.Employee.Lastname.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || e.Employee.Firstname.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "Name":
                    entries = entries.OrderBy(e => e.Employee.Lastname).ToList();
                    break;
                case "name_desc":
                    entries = entries.OrderByDescending(e => e.Employee.Lastname).ToList();
                    break;
                case "Date":
                    entries = entries.OrderBy(e => e.StartWeek).ToList();
                    break;
                case "date_desc":
                    entries = entries.OrderByDescending(e => e.StartWeek).ToList();
                    break;
                case "Requested":
                    entries = entries.OrderBy(e => e.DateCreated).ToList();
                    break;
                case "requested_desc":
                    entries = entries.OrderByDescending(e => e.DateCreated).ToList();
                    break;
                case "Hours":
                    entries = entries.OrderBy(e => e.TotalHours).ToList();
                    break;
                case "hours_desc":
                    entries = entries.OrderByDescending(e => e.TotalHours).ToList();
                    break;
                default:
                    break;
            }

            var entriesVM = _mapper.Map<List<AdminTimeEntryVM>>(entries);

            var model = new AdminTimeEntryViewVM
            {
                TotalRequests = entries.Count,
                ApprovedRequests = entries.Count(q => q.Approved == true),
                PendingRequests = entries.Count(q => q.Approved == null),
                RejectedRequests = entries.Count(q => q.Approved == false),
                Entries = PaginatedList<AdminTimeEntryVM>.Create(entriesVM, pageNumber ?? 1, 5)
            };
            return model;
        }
    }
}
