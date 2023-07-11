using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.Payroll
{
    public class CreatePayrollDto : CreateBaseDto, IPayrollDto
    {
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public decimal NetPay { get; set; }
        public string EmployeeId { get; set; }
    }
}
