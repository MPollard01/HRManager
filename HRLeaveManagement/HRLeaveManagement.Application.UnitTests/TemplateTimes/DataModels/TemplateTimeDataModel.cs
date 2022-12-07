using HRLeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.TemplateTimes.DataModels
{
    internal static class TemplateTimeDataModel
    {
        internal static List<TemplateTime> templates = new List<TemplateTime>
        {
            new TemplateTime
            {
                Id = 1,
                Hours1 = 8,
                Hours2 = 8,
                Hours3 = 8,
                Hours4 = 8,
                Hours5 = 8,
                Hours6 = 8,
                Hours7 = 8,
                Total = 56,
                EmployeeId = "123"
            }
        };
    }
}
