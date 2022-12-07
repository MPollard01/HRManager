using AutoMapper;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagement.Application.UnitTests.TemplateTimes.DataModels;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.Features.TemplateTimes.Handlers.Commands;
using HRLeaveManagment.Application.Features.TemplateTimes.Requests.Commands;
using HRLeaveManagment.Application.Persistence.Contracts;
using HRLeaveManagment.Application.Profiles;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.TemplateTimes.Commands
{
    public class UpdateTemplateTimeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly TemplateTimeDto _templateTimeDto;
        private readonly UpdateTemplateTimeCommandHandler _handler;

        public UpdateTemplateTimeCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateTemplateTimeCommandHandler(_mockUow.Object, _mapper);

            _templateTimeDto = new TemplateTimeDto
            {
                Id = 1,
                Hours1 = 6,
                Hours2 = 8,
                Hours3 = 5,
                Hours4 = 8,
                Hours5 = 8,
                Hours6 = 8,
                Hours7 = 8,
                Total = 51,
                EmployeeId = "123"
            };
        }

        [Fact]
        public async Task Handle_WhenCalled_UpdatesTemplateTime()
        {
            var template = TemplateTimeDataModel.templates[0];

            _mockUow.Setup(r => r.TemplateTimeRepository.Get(_templateTimeDto.Id))
                .ReturnsAsync(template);
        
            _mockUow.Setup(r => r.TemplateTimeRepository.Update(template)).Returns((TemplateTime t) =>
            {
                template = t;
                return Task.CompletedTask;
            });

            var result = await _handler.Handle(new UpdateTemplateTimeCommand { TemplateTimeDto = _templateTimeDto }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            _templateTimeDto.Hours1.ShouldBe(template.Hours1);
            template.Total.ShouldBe(51);
        }
    }
}
