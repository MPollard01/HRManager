using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.Payroll
{
    public class CreatePayrollDto : IPayrollDto
    {
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal NetPay { get; set; }
        public string EmployeeId { get; set; }
    }
}
