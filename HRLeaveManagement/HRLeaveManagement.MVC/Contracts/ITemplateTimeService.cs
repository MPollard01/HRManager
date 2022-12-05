using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ITemplateTimeService
    {
        Task<TemplateTimeVM> GetTemplateTime();
        Task<Response<int>> CreateTemplate(CreateTemplateTimeVM template);
        Task<Response<int>> UpdateTemplate(TemplateTimeVM template);
    }
}
