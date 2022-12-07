using HRLeaveManagement.Api.Controllers;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagment.Application.Persistence.Contracts;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.ApiTests
{
    public class TemplateTimeApiControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ITemplateTimeRepository> _mockRepo;
        private readonly TemplateTimeController _controller;

        public TemplateTimeApiControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mockRepo = MockTemplateTimeRepository.GetTemplates();
            _controller = new TemplateTimeController(_mediator.Object);
        }

        public async void Get_WhenCalled_ReturnsAUsersTemplate()
        {
            var result = await _controller.Get();

            var template = await _mockRepo.Object.GetEmployeeTemplateTime(It.IsAny<string>());
        }
    }
}
