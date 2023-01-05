using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Period Number")]
        public int PeriodNumber { get; set; }

        [Display(Name = "Pay Run")]
        public string PayRun { get; set; }

        [Display(Name = "Pay Period End")]
        public DateTime PayPeriodEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal NetPay { get; set; }
        public string PayPeriod { get; set; }

        [ValidateNever]
        public SelectList Year { get; set; }

        [ValidateNever]
        public SelectList Employees { get; set; }

        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }
    }

    public class PayrollEmployeeViewVM
    {
        public PaginatedList<PayrollVM> Payrolls { get; set; }
        public PayrollEmployeeVM PayrollEmployee { get; set; }
    }

    public class PayrollAdminViewVM
    {
        public PaginatedList<PayrollVM> Payrolls { get; set; }
        public CreatePayrollVM CreatePayroll { get; set; }
    }
}
