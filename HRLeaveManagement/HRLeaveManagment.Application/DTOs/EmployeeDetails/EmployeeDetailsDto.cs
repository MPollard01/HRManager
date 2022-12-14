using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.EmployeeDetails
{
    public class EmployeeDetailsDto : BaseDto, IEmployeeDetailsDto
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public int WorkPhoneNumber { get; set; }
        public int HomePhoneNumber { get; set; }
        public int MobileNumber { get; set; }
        public int WorkMobileNumber { get; set; }
        public string WorkEmail { get; set; }
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
