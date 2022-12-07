using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Queries;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Queries;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Profiles;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.TemplateTimes.Queries
{
    public class GetTemplateTimeRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITemplateTimeRepository> _mockRepo;
        private readonly GetTemplateTimeRequestHandler _handler;
        private readonly Mock<IHttpContextAccessor> _http;

        public GetTemplateTimeRequestHandlerTests()
        {
            _mockRepo = MockTemplateTimeRepository.GetTemplates();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _http = MockHttpContext.GetHttpContext();
            _handler = new GetTemplateTimeRequestHandler(_mockRepo.Object, _mapper, _http.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsAUsersTemplate()
        {
            var result = await _handler.Handle(new GetTemplateTimeRequest(), CancellationToken.None);

            result.ShouldBeOfType<TemplateTimeDto>();
            result.Id.ShouldBe(1);
            result.Hours1.ShouldBe(8);
        }

        [Fact]
        public async Task Handle_WhenNoTemplateIsFound_ReturnsEmptyDto()
        {
            _mockRepo.Setup(r => r.GetEmployeeTemplateTime(It.IsAny<string>())).ReturnsAsync((TemplateTime)null);
            var result = await _handler.Handle(new GetTemplateTimeRequest(), CancellationToken.None);

            result.ShouldBeOfType<TemplateTimeDto>();
            result.Id.ShouldBe(0);
            result.Hours1.ShouldBe(0);
        }
    }
}
