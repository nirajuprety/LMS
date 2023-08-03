using AutoMapper;
using Castle.Core.Logging;
using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using LibraryManagementSystem.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Application.Manager
{
    public class LoginManagerTest
    {
        private readonly LoginManager _loginManager;
        private readonly Mock<ILoginService> _loginServiceMock = new Mock<ILoginService>();
        //private readonly Mock<ILogger<LoginManager>> _loggerMock = new Mock<ILogger<LoginManager>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        public LoginManagerTest()
        {
            _loginManager = new LoginManager(_loginServiceMock.Object, _mapperMock.Object, _configuration.Object);

        }
        [Fact]
        public async Task LoginUser_OnSuccess_ReturnTrue()
        {
            LoginSettingDataInfo.Init();

            //Arrange
            var result = LoginSettingDataInfo.LoginData;
            var request_result = LoginSettingDataInfo.LoginRequestData;
            
            //var expected_result = new ELogin()
            //{
            //    Id = 1,
             //    Email = "abc123@gmail.com",
            //    Password = "abc12345!",
            //    StaffId = 1,
            //};
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "userToken",
                Status = StatusType.Success
            };
            _loginServiceMock.Setup(x => x.ValidateEmail(request_result.Email)).ReturnsAsync(true);
            _loginServiceMock.Setup(x => x.LoginUser(It.IsAny<ELogin>())).ReturnsAsync(true);

            //Act
            var actual_result = await _loginManager.LoginUser(request_result);
            //Assert
            Assert.Equivalent(expected_result.Data, actual_result.Data);

        }
        //validateEmail
        [Fact]
        public async Task ValidEmail_ReturnTrue()
        {
            //LoginSettingDataInfo.Init();

            //Arrange
            var model = new LoginRequest()
            {
                Email = "admin123!U1@gmail.com",
                Password = "admin123!"
            };
            var eLogin = new ELogin()
            {
                Email = model.Email,
                Password = model.Password
            };
            var exprected_result = true;
            _loginServiceMock.Setup(x => x.ValidateEmail(model.Email)).ReturnsAsync(exprected_result);

            _loginServiceMock.Setup(x => x.LoginUser(It.IsAny<ELogin>())).ReturnsAsync(exprected_result);
            //Act
            var actual_result = await _loginManager.LoginUser(model);

            //Assert
            Assert.Equivalent(exprected_result, actual_result.Data);
        }
       
    }
}
