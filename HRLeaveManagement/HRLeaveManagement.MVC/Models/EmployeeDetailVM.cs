using System.ComponentModel;

namespace HRLeaveManagement.MVC.Models
{
    public class EmployeeDetailVM
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        [DisplayName("Address Line 1")]
        public string Address1 { get; set; }

        [DisplayName("Address Line 2")]
        public string Address2 { get; set; }
        public string Town { get; set; }

        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        [DisplayName("Work Phone Number")]
        public long WorkPhoneNumber { get; set; }

        [DisplayName("Home Phone Number")]
        public long HomePhoneNumber { get; set; }

        [DisplayName("Mobile Number")]
        public long MobileNumber { get; set; }

        [DisplayName("Work Mobile Number")]
        public long WorkMobileNumber { get; set; }

        [DisplayName("Work Email")]
        public string WorkEmail { get; set; }

        [DisplayName("Personal Email")]
        public string PersonalEmail { get; set; }

        [DisplayName("National Insurance Number")]
        public string NINumber { get; set; }

        [DisplayName("Tax Code")]
        public string TaxCode { get; set; }

        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Account Name")]
        public string AccountName { get; set; }

        [DisplayName("Account Number")]
        public int AccountNumber { get; set; }

        [DisplayName("Sort Code")]
        public int SortCode { get; set; }

        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }
    }

    public class PayrollEmployeeVM
    {
        public string NINumber { get; set; }
        public string TaxCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public int SortCode { get; set; }
        public string BankAddress { get; set; }
    }

    public class CreateEmployeeDetailVM
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkEmail { get; set; }
    }
}
