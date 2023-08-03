using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using Library.UnitTest.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest.Infrastructure.Service
{
    public class StaffServiceTest : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task AddStaff_OnSuccess_ReturnsId()

        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                StaffDataInfo.Init();
                var request = StaffDataInfo.eStaffRequest;
                bool EXPECTED_RESULT = true;
                //  var response = StaffDataInfo.eStaffResponseWithId;

                //var EXPECTED_RESULT = response.Id;

                //act
                var result = await service.AddStaff(request);

                //assert

                Assert.Equivalent(EXPECTED_RESULT, (result != 0 ? true : false));
            }
        }

        [Fact]
        public async Task CreateLogin_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                StaffDataInfo.Init();
                var request = StaffDataInfo.eLogin;

                var expected_result = true;

                //act
                var result = await service.CreateLogin(request);
                //assert
                Assert.Equivalent(expected_result, result);
            }

        }

        [Fact]
        public async Task UpdateUser_OnSuccess_ReturnTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                StaffDataInfo.Init();
                var request = StaffDataInfo.eLogin;
                var expected_result = true;

                //Act
                var result = await service.UpdateUser(request);

                //Assert
                Assert.Equivalent(expected_result, result);
            }

        }

        [Fact]
        public async Task GetAllStaff_OnSuccess_StaffInfo()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                StaffDataInfo.Init();
                var expected_result = StaffDataInfo.eStaffList;

                //act
                var result = await service.GetAllStaff();

                //Assert
                Assert.Equivalent(expected_result, result);

            }
        }

        [Fact]
        public async Task GetStaffById_OnSuccess_ReturnsStaffInfo()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                int id = 1;
                StaffDataInfo.Init();
                var response = StaffDataInfo.eStaffResponse;

                var expected_result = response;

                //act
                var result = await service.GetStaffById(id);

                //Assert
                Assert.Equivalent(expected_result, result);
            }
        }

        [Fact]

        public async Task UpdateStaff_OnSuccess_Returnstrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange
                var request = StaffDataInfo.eStaffUpdateRequest;

                var expected_result = true;

                //act
                var result = await service.UpdateStaff(request);
                //assert
                Assert.Equivalent(expected_result, result);
            }
        }

        [Fact]
        public async Task DeleteStaff_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);

                //arrange 
                int id = 1;
                var request = StaffDataInfo.eStaffRequest;

                var expected_result = true;
                //act
                var result = await service.DeleteStaff(id);

                //assert
                Assert.Equivalent(expected_result, result);

            }
        }

        [Fact]
        public async Task DeleteUser_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);
                //arrange
                int id = 1;
                var request = StaffDataInfo.eLogin;
                var expected_result = true;
                //act
                var actual_result = await service.DeleteUser(id);

                //assert
                Assert.Equivalent(expected_result, actual_result);
            }
        }

        [Fact]
        public async Task IsUniqueEmail_OnSuccess_ReturnsFalse()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);
                //arrange
                var request = StaffDataInfo.eStaffRequest;
                var expected_result = false;
                //act
                var actual_result = await service.IsUniqueEmail(request.Email);
                //Assert
                Assert.Equivalent(expected_result, actual_result);
            }
        }

        [Fact]
        public async Task IsUniqueStaffCode_OnSuccess_ReturnsFalse()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StaffService(factory);
                //arrange
                var request = StaffDataInfo.eStaffRequest;
                var expected_result = false;
                //act
                var actual_result = await service.IsUniqueStaffCode(request.StaffCode);

                //Assert
                Assert.Equivalent(expected_result, actual_result);

            }
        }
    }
}
