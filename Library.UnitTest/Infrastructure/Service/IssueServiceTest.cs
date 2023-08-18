using Library.Domain.Entities;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using Library.UnitTest.Infrastructure.Data;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest.Infrastructure.Service
{
    public class IssueServiceTest : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task AddIssueService_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var requestresult = IssueDataInfo.IssueTableDataAdd;
                bool EXPECTED_RESULT = true;

                //act
                var result = await service.AddIssuedService(requestresult);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }
        //false test
        [Fact]
        public async Task AddIssueService_OnFail_ReturnsFalse()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var requestresult = IssueDataInfo.IssueTableDataAddFalse;
                bool EXPECTED_RESULT = false;

                //act
                var result = await service.AddIssuedService(requestresult);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }
        [Fact]
        public async Task DeleteIssueService_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                bool EXPECTED_RESULT = true;

                //act
                var result = await service.DeleteIssuedService(1);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }
        [Fact]
        public async Task DeleteIssueService_OnFail_ReturnsFalse()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                bool EXPECTED_RESULT = false;

                //act
                var result = await service.DeleteIssuedService(0);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }
        [Fact]
        public async Task GetIssueServiceById_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var expected_result = IssueDataInfo.IssueTableData;
                //act
                var result = await service.GetIssuedServiceById(1);

                //assert
                Assert.Equivalent(expected_result.Id, result.Id);
            }

        }

        [Fact]
        public async Task GetIssueServiceById_OnFail_ReturnsFalse ()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var expected_result = IssueDataInfo.IssueTableDataAddFalse;
                //act
                var result = await service.GetIssuedServiceById(0);

                //assert
                Assert.Equivalent(expected_result, result);
            }

        }
        [Fact]
        public async Task GetIssueServices_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
              
                var expected_result = IssueDataInfo.IssueTableResponseList;

                //act
                var result = await service.GetIssuedServices();

                //assert
                Assert.Equivalent(expected_result, result);
            }

        }
          [Fact]
        public async Task GetIssueServices_OnFail_ReturnsNull()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
              
                var expected_result = new List<EIssueTable>();

                //act
                var result = await service.GetIssuedServices();

                //assert
                Assert.Equivalent(expected_result, result);
            }

        }

           [Fact]
        public async Task UpdateIssueService_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var requestresult = IssueDataInfo.IssueTableData;
                bool EXPECTED_RESULT = true;

                //act
                var result = await service.UpdateIssuedService(requestresult);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }

        [Fact]
        public async Task UpdateIssueService_OnFail_ReturnsFalse()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new IssueService(factory);

                //arrange
                var requestresult = IssueDataInfo.IssueTableDataAddFalse;
                bool EXPECTED_RESULT = false;

                //act
                var result = await service.UpdateIssuedService(requestresult);

                //assert
                Assert.Equivalent(EXPECTED_RESULT, result);
            }

        }




    }
}
