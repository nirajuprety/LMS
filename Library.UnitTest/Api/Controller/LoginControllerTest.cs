using Castle.Core.Logging;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using LibraryManagementSystem.Controllers;

using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Library.UnitTest.Infrastructure.Data;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Api.Controller
{
    public class LoginControllerTest : IClassFixture<DatabaseFixture>
    {
        //suru ma always mock the manager and other dependencies
        private readonly LoginController _loginController = null;
        private readonly Mock<ILoginManager> _loginManager = new Mock<ILoginManager>();
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        private readonly Mock<ILogger<LoginController>> _logger = new Mock<ILogger<LoginController>>();

        public LoginControllerTest()
        {
            _loginController = new LoginController(_loginManager.Object, _configuration.Object, _logger.Object);
        }
        [Fact]
        public async Task Login_Success_ReturnTrue()
        {
            LoginSettingDataInfo.Init();
            //Arrange
            var LoginRequest = LoginSettingDataInfo.LoginRequestData;

            var expected_result = new ServiceResult<bool>
            {
                Data = true,
                Message = "LoginSuccess",
                Status = StatusType.Success
            };
            _loginManager.Setup(service => service.LoginUser(LoginRequest)).ReturnsAsync(expected_result);

            //Act
            var actual_result = await _loginController.LoginUser(LoginRequest);

            //Assert
            Assert.NotNull(actual_result);
            //Assert.Equal(200, actual_result.StatusCode);
            Assert.Equal(expected_result.Data, actual_result.Data);
        }
    }
}
