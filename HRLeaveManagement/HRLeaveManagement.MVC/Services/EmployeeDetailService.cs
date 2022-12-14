using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class EmployeeDetailService : BaseHttpService, IEmployeeDetailService
    {
        private readonly IMapper _mapper;
        public EmployeeDetailService(IClient client, 
            ILocalStorageService localStorageService, IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateEmployeeDetail(CreateEmployeeDetailVM employeeDetail)
        {
            try
            {
                var response = new Response<int>();
                var createEmployeeDetail = _mapper.Map<CreateEmployeeDetailsDto>(employeeDetail);
                AddBearerToken();
                var apiResponse = await _client.EmployeeDetailPOSTAsync(createEmployeeDetail);

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

        public async Task<EmployeeDetailVM> GetEmployeeDetails()
        {
            AddBearerToken();
            var employeeDetails = await _client.EmployeeDetailGETAsync();
            return _mapper.Map<EmployeeDetailVM>(employeeDetails);
        }

        public async Task<PayrollEmployeeVM> GetPayrollEmployeeDetails()
        {
            AddBearerToken();
            var employeeDetails = await _client.PayrollAsync();
            return _mapper.Map<PayrollEmployeeVM>(employeeDetails);
        }

        public async Task<Response<int>> UpdateEmployeeDetail(EmployeeDetailVM employeeDetail)
        {
            try
            {
                var updateEmployeeDetail = _mapper.Map<UpdateEmployeeDetailsDto>(employeeDetail);
                AddBearerToken();
                await _client.EmployeeDetailPUTAsync(updateEmployeeDetail);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
