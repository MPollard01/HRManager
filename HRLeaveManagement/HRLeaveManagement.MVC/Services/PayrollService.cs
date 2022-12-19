using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class PayrollService : BaseHttpService, IPayrollService
    {
        private readonly IMapper _mapper;
        public PayrollService(IClient client, 
            ILocalStorageService localStorageService, IMapper mapper)
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreatePayroll(CreatePayrollVM payrollVM)
        {
            try
            {
                var response = new Response<int>();
                var dto = _mapper.Map<CreatePayrollDto>(payrollVM);
                AddBearerToken();

                var apiResponse = await _client.PayrollPOSTAsync(dto);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<PayrollVM> GetPayroll(int id)
        {
            AddBearerToken();
            var payrolls = await _client.PayrollGETAsync(id);
            return _mapper.Map<PayrollVM>(payrolls);
        }

        public async Task<List<PayrollVM>> GetPayrollAdminList()
        {
            AddBearerToken();
            var payrolls = await _client.PayrollAllAsync(false);
            return _mapper.Map<List<PayrollVM>>(payrolls);
        }

        public async Task<List<PayrollVM>> GetPayrollList()
        {
            AddBearerToken();
            var payrolls = await _client.PayrollAllAsync(true);
            return _mapper.Map<List<PayrollVM>>(payrolls);
        }
    }
}
