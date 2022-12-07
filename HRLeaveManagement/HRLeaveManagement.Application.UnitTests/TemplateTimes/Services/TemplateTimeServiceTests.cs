using AutoMapper;
using HRLeaveManagement.Application.UnitTests.TemplateTimes.DataModels;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagement.MVC;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services;
using HRLeaveManagement.MVC.Services.Base;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.TemplateTimes.Services
{
    public class TemplateTimeServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IClient> _mockClient;
        private readonly TemplateTimeService _sut;
        private readonly TemplateTimeVM _templateTimeVM;

        public TemplateTimeServiceTests()
        {
            _mockClient = new Mock<IClient>();
            var storageService = new Mock<ILocalStorageService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _sut = new TemplateTimeService(_mockClient.Object, storageService.Object, _mapper);

            _templateTimeVM = new TemplateTimeVM
            {
                EmployeeId = null,
                Hours1 = 8,
                Hours2 = 8,
                Hours3 = 8,
                Hours4 = 8,
                Hours5 = 8,
                Hours6 = 8,
                Hours7 = 8,
                Total = 56
            };
        }

        [Fact]
        public async Task GetTemplateTime_WhenCalled_ReturnsUsersTemplate()
        {
            _mockClient.Setup(c => c.TemplateTimeGETAsync()).ReturnsAsync(TemplateTimeDtoDataModel.templateTimeDtos[0]);

            var result = await _sut.GetTemplateTime();

            result.ShouldBeOfType<TemplateTimeVM>();
            result.Hours1.ShouldBe(8);
        }

        [Fact]
        public async Task CreateTemplateTime_WhenCalled_ReturnsSuccessResponse()
        {
            _mockClient.Setup(c => c.TemplateTimePOSTAsync(It.IsAny<CreateTemplateTimeDto>()))
                .ReturnsAsync(new BaseCommandResponse { Id = 1, Success = true });

            var result = await _sut.CreateTemplate(_templateTimeVM);

            result.Success.ShouldBeTrue();
            result.Data.ShouldBe(1);
        }

        [Fact]
        public async Task CreateTemplateTime_WhenPOSTFails_ReturnsFailedResponse()
        {
            _mockClient.Setup(c => c.TemplateTimePOSTAsync(It.IsAny<CreateTemplateTimeDto>()))
                .ReturnsAsync(new BaseCommandResponse 
                { Id = 1, Success = false, Errors = new List<string> { It.IsAny<string>() } });

            var result = await _sut.CreateTemplate(_templateTimeVM);

            result.Success.ShouldBeFalse();
        }
    }
}
