using HRLeaveManagment.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails
{
    public class PayrollEmployeeDetailsDto : BaseDto, IEmployeeDetailsDto
    {
        public string EmployeeId { get; set; }
        public string NINumber { get; set; }
        public string TaxCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public int SortCode { get; set; }
        public string BankAddress { get; set; }
    }
}
