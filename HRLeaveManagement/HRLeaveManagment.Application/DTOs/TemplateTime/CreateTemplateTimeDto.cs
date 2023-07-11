using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.TemplateTime
{
    public class CreateTemplateTimeDto : CreateBaseDto, ITemplateTimeDto
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
