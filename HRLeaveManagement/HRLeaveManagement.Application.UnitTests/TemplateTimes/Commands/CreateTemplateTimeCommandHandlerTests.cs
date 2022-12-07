using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Commands;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Profiles;
using HRLeaveManagment.Application.Responses;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.TemplateTimes.Commands
{
    public class CreateTemplateTimeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IHttpContextAccessor> _mockContextAccessor;
        private readonly CreateTemplateTimeDto _templateTimeDto;
        private readonly CreateTemplateTimeCommandHandler _handler;

        public CreateTemplateTimeCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockContextAccessor = MockHttpContext.GetHttpContext();

            _handler = new CreateTemplateTimeCommandHandler(_mockUow.Object, _mockContextAccessor.Object, _mapper);

            _templateTimeDto = new CreateTemplateTimeDto
            {
                Hours1 = 8,
                Hours2 = 8,
                Hours3 = 8,
                Hours4 = 8,
                Hours5 = 8,
                Hours6 = 0,
                Hours7 = 0,
            };
        }

        [Fact]
        public async Task Handle_WhenCalled_AddsTemplateTimeToRepository()
        {
            var result = await _handler.Handle(new CreateTemplateTimeCommand { TemplateTimeDto = _templateTimeDto }, CancellationToken.None);
            var templates = await _mockUow.Object.TemplateTimeRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();
            templates.Count.ShouldBe(2);
        }
    }
}
