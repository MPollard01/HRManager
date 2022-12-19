using HRLeaveManagment.Application.DTOs.Common;
using HRLeaveManagment.Application.Models.Identity;

namespace HRLeaveManagment.Application.DTOs.Payroll
{
    public class PayrollDto : BaseDto, IPayrollDto
    {
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal NetPay { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
