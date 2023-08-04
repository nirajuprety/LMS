using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using Library.UnitTest.Infrastructure.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest.Infrastructure.Service
{
    public class LoginServiceTest : IClassFixture<DatabaseFixture>
    {
        private readonly LoginService? _loginService = null;
        private readonly DatabaseFixture _fixture;

        public LoginServiceTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task LoginUser_OnSuccess_ReturnsData()
        {
            //Arrange
            //DatabaseFixture _fixture = new DatabaseFixture();

            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);

                //test data
                var request_result = new ELogin
                {
                    Id = 1,
                    Email = "user@gmail.com",
                    Password = "user1234!",
                    StaffId = 1,
                };
                request_result.Password = service.HashPassword(request_result.Password);
                //var request_result = LoginSettingDataInfo.LoginData;
                //request_result.Password = HashPassword(request_result.Password);
                bool EXPECTED_RESULT = true;

                //act
                var result = await service.LoginUser(request_result);
                //assert

                Assert.Equivalent(EXPECTED_RESULT, result);

            }

        }
        [Fact]
        public async Task ValidateEmail_OnSuccess_ReturnTrue()
        {
            DatabaseFixture fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);
                //Arrange
                var request = LoginSettingDataInfo.LoginData;
                var expected_result = true;

                //Act
                var actual_result = await service.ValidateEmail(request.Email);

                //Assert
                Assert.Equivalent(expected_result, actual_result);
            }
        }

        [Fact]
        public async Task GetUserById_OnSuccss_ReturnsUserInfo()
        {
            DatabaseFixture fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);
                //Arrange
                int id = 1;
                var response = LoginSettingDataInfo.LoginData;
                var expected_result = LoginSettingDataInfo.LoginData;

                //Act
                var actual_result = await service.GetUserById(id);

                //Assert
                Assert.Equivalent(expected_result, actual_result);
            }
        }

        [Fact]
        public async Task AddLogin_OnSuccess_ReturnTrue()
        {
            DatabaseFixture fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);
                //Arrange
                var request = LoginSettingDataInfo.LoginData;
                var expected_result = true;

                //Act
                var actual_result = await service.AddLogin(request, request.StaffId);

                //assert
                Assert.Equivalent(expected_result, actual_result);
            }
        }

        [Fact]

        public async Task GetUserRole_OnSuccess_ReturnsStaffRoleasAdmin()
        {
            DatabaseFixture fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);

                //Arrange
                var request = LoginSettingDataInfo.LoginData;
                var expected_result = StaffType.Admin;

                //Act
                var actual_result = await service.GetUserRole(request.Email);

                //Assert
                Assert.Equivalent(expected_result, actual_result);



            }
        }

        [Fact]

        public async Task GetUserRole_OnSuccess_ReturnsStaffRoleasStaff()

        {
            DatabaseFixture fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new LoginService(factory);

                //Arrange
                var request = LoginSettingDataInfo.LoginStaffData;
                var expected_result = StaffType.Admin;

                //Act
                var actual_result = await service.GetUserRole(request.Email);

                //Assert
                Assert.Equivalent(expected_result, actual_result);



            }
        }
    }
}
