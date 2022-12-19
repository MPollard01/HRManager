using HRLeaveManagement.Clean.Domain.Common;

namespace HRLeaveManagement.Clean.Domain
{
    public class Payroll : BaseDomainEntity
    {
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal NetPay { get; set; }
        public string EmployeeId { get; set; }
    }
}
