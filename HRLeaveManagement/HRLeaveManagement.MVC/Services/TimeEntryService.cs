using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
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

        public async Task<Response<int>> CreateTimeEntry(CreateTimeEntryVM timeEntry)
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

        public async Task<TimeEntryVM> GetTimeEntry(int id)
        {
            AddBearerToken();
            var timeEntries = await _client.TimeEntryGETAsync(id);
            return _mapper.Map<TimeEntryVM>(timeEntries);
        }

        public async Task<TimeEntryVM> GetTimeEntryByDate(DateTime date)
        {
            AddBearerToken();
            var timeEntry = await _client.TimeEntryAllAsync(true);
            var t = timeEntry.FirstOrDefault(e => e.StartWeek == date);

            if(t == null)
            {
                var entry = new TimeEntryVM
                {
                    StartWeek = date,
                    EndWeek = date.AddDays(6),
                    Hours = new List<HoursDayVM>()
                };
                
                return entry;
            }

            return _mapper.Map<TimeEntryVM>(t);
        }
    }
}
