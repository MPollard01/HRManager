﻿using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails
{
    public class UpdateEmployeeDetailsDto : UpdateBaseDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public long WorkPhoneNumber { get; set; }
        public long HomePhoneNumber { get; set; }
        public long MobileNumber { get; set; }
        public long WorkMobileNumber { get; set; }
        public string PersonalEmail { get; set; }
        public string NINumber { get; set; }
        public string TaxCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public int SortCode { get; set; }
        public string BankAddress { get; set; }
    }
}
