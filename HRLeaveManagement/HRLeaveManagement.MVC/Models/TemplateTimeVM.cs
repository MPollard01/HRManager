using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HRLeaveManagement.MVC.Models
{
    public class TemplateTimeVM : CreateTemplateTimeVM
    {
        public int Id { get; set; }
        [ValidateNever]
        public string EmployeeId { get; set; }
    }

    public class CreateTemplateTimeVM
    {
        public int Hours1 { get; set; }
        public int Hours2 { get; set; }
        public int Hours3 { get; set; }
        public int Hours4 { get; set; }
        public int Hours5 { get; set; }
        public int Hours6 { get; set; }
        public int Hours7 { get; set; }
        public int Total { get; set; }
    }
}
