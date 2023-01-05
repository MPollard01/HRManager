using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

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

                payrollVM.PayPeriodEnd = DateTime.Parse(payrollVM.PayPeriod);

                var periods = new Dictionary<int, int>
                {
                    { 04, 1 },
                    { 05, 2 },
                    { 06, 3 },
                    { 07, 4 },
                    { 08, 5 },
                    { 09, 6 },
                    { 10, 7 },
                    { 11, 8 },
                    { 12, 9 },
                    { 01, 10 },
                    { 02, 11 },
                    { 03, 12 },
                };

                payrollVM.PeriodNumber = periods[payrollVM.PayPeriodEnd.Month];
                payrollVM.PayRun = payrollVM.PayPeriodEnd.ToString("MMMM-yyyy");

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

        public async Task<PayrollAdminViewVM> GetPayrollAdminList(string searchString, string sortOrder, int? pageNumber)
        {
            AddBearerToken();
            var payrolls = await _client.PayrollAllAsync(false);

            if (!string.IsNullOrEmpty(searchString))
            {
                payrolls = payrolls.Where(e => e.Employee.Lastname.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || e.Employee.Firstname.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "Name":
                    payrolls = payrolls.OrderBy(e => e.Employee.Lastname).ToList();
                    break;
                case "name_desc":
                    payrolls = payrolls.OrderByDescending(e => e.Employee.Lastname).ToList();
                    break;
                case "Date":
                    payrolls = payrolls.OrderBy(e => e.PayPeriodEnd).ToList();
                    break;
                case "date_desc":
                    payrolls = payrolls.OrderByDescending(e => e.PayPeriodEnd).ToList();
                    break;
                case "Pay":
                    payrolls = payrolls.OrderBy(e => e.NetPay).ToList();
                    break;
                case "pay_desc":
                    payrolls = payrolls.OrderByDescending(e => e.NetPay).ToList();
                    break;
                default:
                    break;
            }

            var payrollVMs = _mapper.Map<List<PayrollVM>>(payrolls);
            var employees = await _client.EmployeeAllAsync();
            var employeeList = new SelectList(employees, "Id", "Lastname");
            var years = new List<string>();

            for (int year = 2015; year < DateTime.Now.Year; year++)
                years.Add($"{year}/{year + 1}");

            var yearList = new SelectList(years);

            return new PayrollAdminViewVM 
            { 
                Payrolls = PaginatedList<PayrollVM>.Create(payrollVMs, pageNumber ?? 1, 10),
                CreatePayroll = new CreatePayrollVM 
                { 
                    Employees = employeeList,
                    Year = yearList
                } 
            };
        }

        public async Task<List<PayrollVM>> GetPayrollList(string sortOrder)
        {
            AddBearerToken();
            var payrolls = await _client.PayrollAllAsync(true);

            switch (sortOrder)
            {
                case "Date":
                    payrolls = payrolls.OrderBy(e => e.PayPeriodEnd).ToList();
                    break;
                case "date_desc":
                    payrolls = payrolls.OrderByDescending(e => e.PayPeriodEnd).ToList();
                    break;
                case "Pay":
                    payrolls = payrolls.OrderBy(e => e.NetPay).ToList();
                    break;
                case "pay_desc":
                    payrolls = payrolls.OrderByDescending(e => e.NetPay).ToList();
                    break;
                default:
                    break;
            }

            return _mapper.Map<List<PayrollVM>>(payrolls);
        }
    }
}
