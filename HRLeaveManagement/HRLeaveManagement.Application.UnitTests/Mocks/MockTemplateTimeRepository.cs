using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.Persistence.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    internal class MockTemplateTimeRepository
    {
        public static Mock<ITemplateTimeRepository> GetTemplates()
        {
            var templates = new List<TemplateTime>
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

            var mockRepo = new Mock<ITemplateTimeRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(templates);
            mockRepo.Setup(r => r.GetEmployeeTemplateTime(It.IsAny<string>())).ReturnsAsync(templates[0]);
            mockRepo.Setup(r => r.Add(It.IsAny<TemplateTime>())).ReturnsAsync((TemplateTime templateTime) =>
            {
                templates.Add(templateTime);
                return templateTime;
            });

            return mockRepo;
        }
    }
}
