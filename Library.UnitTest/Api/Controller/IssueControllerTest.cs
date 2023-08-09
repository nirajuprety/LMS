using Confluent.Kafka;
using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using LibraryManagementSystem.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Api.Controller
{
    public class IssueControllerTest 
    {
        private readonly IssueController _controller;
        private readonly Mock<IIssueManager> _issueManagerMock;

        public IssueControllerTest()
        {
            _issueManagerMock = new Mock<IIssueManager>();
            _controller = new IssueController(_issueManagerMock.Object);
        }
        [Fact]
        public async Task AddIssue_OnSuccess_ReturnStausSuccess()
        {
            //arraange
            IssueDataInfo.Init();
            var model = IssueDataInfo.IssueRequestData;
            // Create the expected success message
            //var expectedSuccessMessage = "Book issue added successfully.";
            var expected_result = new ServiceResult<bool>()
            {
                Message = "OK",
                Data = true,
                Status = StatusType.Success,
            };
            string id = "1";

            // act
            _issueManagerMock.Setup(x => x.AddIssue(model,id)).ReturnsAsync(expected_result);
            var actual_result = await _controller.AddIssue(model) ;

            // Assert
             Assert.Equal(actual_result.Status, expected_result.Status);          
        }
        [Fact]
        public async Task AddIssue_OnFail_ReturnStausFaill()
        {
            //arraange
            IssueDataInfo.Init();
            var model = IssueDataInfo.IssueRequestData;
            // Create the expected success message
            //var expectedSuccessMessage = "Book issue added successfully.";
            var expected_result = new ServiceResult<bool>()
            {
                Message = "Failed",
                Data = false,
                Status = StatusType.Failure,
            };
            string id = "1";

            // act
            _issueManagerMock.Setup(x => x.AddIssue(model, id)).ReturnsAsync(expected_result);
            var actual_result = await _controller.AddIssue(model);

            // Assert
            Assert.Equal(actual_result.Status, expected_result.Status);
        }
    }
}
