using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Range(100000, int.MaxValue, ErrorMessage = "Must have at least 6 numbers")]
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
        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [DisplayName("Address Line 1")]
        public string Address1 { get; set; }

        [DisplayName("Address Line 2")]
        [Required]
        public string Address2 { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        [Required]
        [DisplayName("Work Phone Number")]
        public long WorkPhoneNumber { get; set; }

        [Required]
        [DisplayName("Home Phone Number")]
        public long HomePhoneNumber { get; set; }

        [Required]
        [DisplayName("Mobile Number")]
        public long MobileNumber { get; set; }

        [Required]
        [DisplayName("Work Mobile Number")]
        public long WorkMobileNumber { get; set; }

        [Required]
        [DisplayName("Personal Email")]
        public string PersonalEmail { get; set; }

        [Required]
        [DisplayName("National Insurance Number")]
        public string NINumber { get; set; }

        [Required]
        [DisplayName("Tax Code")]
        public string TaxCode { get; set; }

        [Required]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [Required]
        [DisplayName("Account Name")]
        public string AccountName { get; set; }

        [Required]
        [DisplayName("Account Number")]
        public int AccountNumber { get; set; }

        [Required]
        [DisplayName("Sort Code")]
        public int SortCode { get; set; }

        [Required]
        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }
    }
}
