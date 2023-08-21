using AutoMapper;
using Castle.Core.Logging;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;
using Xunit;
using Library.Domain.Entities;
using Library.Application.DTO.Request;
using Library.UnitTest.Infrastructure.Data;
using Library.Application.DTO.Response;
using Library.Application.Kafka.Interface;

namespace Library.UnitTest.Application.Manager
{
    public class IssueManagerTest
    {
        private readonly IssueManager _issueManager;
        private readonly Mock<IIssuedService> _issueService = new Mock<IIssuedService>();
        private readonly Mock<IMemberService> _memberService = new Mock<IMemberService>();
        private readonly Mock<IAddIssueDetailsProducer> _detailproducer = new Mock<IAddIssueDetailsProducer>();
        

        private readonly Mock<ILogger<IssueManager>> _logger= new Mock<ILogger<IssueManager>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public IssueManagerTest() 
        {
            _issueManager = new IssueManager(_issueService.Object, _mapperMock.Object, _logger.Object,_memberService.Object,_detailproducer.Object);
        }
        [Fact]
        public async Task AddIssue_OnSuccess_ReturnTrue()
        {
            //arrange
            IssueDataInfo.Init();
            //IssueReq data
            var issuerequest = IssueDataInfo.IssueRequestData;
            //EIssueTable data
            var issueTableRequest = IssueDataInfo.IssueTableData;
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Ok",
                Status = StatusType.Success
            };
            _mapperMock.Setup(x => x.Map<EIssueTable>(It.IsAny<IssueRequest>())).Returns(issueTableRequest);
            _issueService.Setup(x => x.AddIssuedService(issueTableRequest)).ReturnsAsync(expected_result.Data);
         
            //act
            var actualResult = await _issueManager.AddIssue(issuerequest, "1");

            //assert
            Assert.Equivalent(actualResult.Data, expected_result.Data);
            
        }
        [Fact]
        public async Task Delete_OnSuccess_ReturnTrue()
        {
            //arrange
            IssueDataInfo.Init();
            //IssueReq data
            var issuerequest = IssueDataInfo.IssueRequestData;
            //EIssueTable data
            var issueTableRequest = IssueDataInfo.IssueTableData;
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Ok",
                Status = StatusType.Success
            };
            _issueService.Setup(x => x.GetIssuedServiceById(1)).ReturnsAsync(issueTableRequest);
            _issueService.Setup(x => x.DeleteIssuedService(1)).ReturnsAsync(true);


            //act
            var actualResult = await _issueManager.DeleteIssue(1, "1");

            //assert
            Assert.Equivalent(actualResult.Data, expected_result.Data);

        }
        [Fact]
        public async Task GetIssueById_OnSuccess_ReturnTrue()
        {
            //arrange
            IssueDataInfo.Init();
            //IssueReq data
            var issueResponse = IssueDataInfo.IssueResponseData;
            //EIssueTable data
            var issueTableRequest = IssueDataInfo.IssueTableData;
            var expected_result = new ServiceResult<IssueResponse>()
            {
                Data = issueResponse,
                Message = "Ok",
                Status = StatusType.Success
            };
            _issueService.Setup(x => x.GetIssuedServiceById(1)).ReturnsAsync(issueTableRequest);
            _mapperMock.Setup(x => x.Map<IssueResponse>(issueTableRequest)).Returns(issueResponse);

            //act
            var actualResult = await _issueManager.GetIssueById(1);

            //assert
            Assert.Equivalent(actualResult.Data, expected_result.Data);

        }

        [Fact]
        public async Task GetIssues_OnSuccess_ReturnTrue()
        {
            //arrange
            IssueDataInfo.Init();
            //IssueReq data
            var issueResponse = IssueDataInfo.IssueResponseDataList;
            //EIssueTable data
            var IssueTableResponseList = IssueDataInfo.IssueTableResponseList;
            var expected_result = new ServiceResult<List<IssueResponse>>()
            {
                Data = issueResponse,
                Message = "Ok",
                Status = StatusType.Success
            };
            _issueService.Setup(x => x.GetIssuedServices()).ReturnsAsync(IssueTableResponseList);

            _mapperMock.Setup(x => x.Map<List<IssueResponse>>(IssueTableResponseList)).Returns(issueResponse);

            //act
            var actualResult = await _issueManager.GetIssues();

            //assert
            Assert.Equivalent(actualResult.Data, expected_result.Data);

        }

         [Fact]
        public async Task UpdatesIssues_OnSuccess_ReturnTrue()
        {
            //arrange
            IssueDataInfo.Init();
            //IssueReq data
            var issueRequest = IssueDataInfo.IssueRequestData;
            //EIssueTable data
            var IssueTableData = IssueDataInfo.IssueTableData;
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Ok",
                Status = StatusType.Success
            };
            _issueService.Setup(x => x.GetIssuedServiceById(1)).ReturnsAsync(IssueTableData);
            _mapperMock.Setup(x => x.Map<EIssueTable>(It.IsAny<IssueRequest>())).Returns(IssueTableData);
            //_mapperMock.Setup(x => x.Map<EIssueTable>(issueTableRequest)).Returns(issueResponse);
            _issueService.Setup(x => x.UpdateIssuedService(IssueTableData)).ReturnsAsync(true);

            //act
            var actualResult = await _issueManager.UpdateIssue(issueRequest, "1");

            //assert
            Assert.Equivalent(actualResult.Data, expected_result.Data);

        }


    }
}
