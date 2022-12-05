using HRLeaveManagment.Application.Constants;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    internal class MockHttpContext
    {
        public static Mock<IHttpContextAccessor> GetHttpContext()
        {
            var mockContextAccessor = new Mock<IHttpContextAccessor>();

            var userId = "123";
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[] { new Claim(CustomClaimTypes.Uid, userId) }));

            var context = new DefaultHttpContext { User = user };

            mockContextAccessor.Setup(r => r.HttpContext).Returns(context);

            return mockContextAccessor;
        }
    }
}
