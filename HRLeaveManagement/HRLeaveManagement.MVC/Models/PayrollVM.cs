using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRLeaveManagement.MVC.Models
{
    public class PayrollVM
    {
        public int Id { get; set; }
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal NetPay { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

    public class CreatePayrollVM
    {
        public int PeriodNumber { get; set; }
        public string PayRun { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal NetPay { get; set; }
        public SelectList Employees { get; set; }
        public string EmployeeId { get; set; }
    }

    public class PayrollEmployeeViewVM
    {
        public List<PayrollVM> Payrolls { get; set; }
        public PayrollEmployeeVM PayrollEmployee { get; set; }
    }

    public class PayrollAdminViewVM
    {
        public List<PayrollVM> Payrolls { get; set; }
        public CreatePayrollVM CreatePayroll { get; set; }
    }
}
