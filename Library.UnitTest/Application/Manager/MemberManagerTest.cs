using AutoMapper;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;
using Xunit;
using Library.Application.DTO.Response;
using Library.Application.DTO.Request;
using Library.Domain.Enum;

namespace Library.UnitTest.Application.Manager
{
    public class MemberManagerTest
    {
        private readonly MemberManager _memberManager;
        private readonly Mock<IMemberService> _memberServiceMock = new Mock<IMemberService>();
        private readonly Mock<ILoginService> _loginServiceMock = new Mock<ILoginService>();
        //private readonly Mock<ILogger<LoginManager>> _loggerMock = new Mock<ILogger<LoginManager>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        public MemberManagerTest()
        {
            _memberManager = new MemberManager(_memberServiceMock.Object, _mapperMock.Object, _loginServiceMock.Object);

        }
        [Fact]
        public async Task GetMembers_Return_MemberResponseList()
        {
            MemberEntityData.Init();
            //Arrange
            var result = MemberEntityData.ListOfMembers;
            var MemberList = new List<MemberResponse>()
            {
                new MemberResponse()
                {
                    Email = "Jhapa Nepal",
                    FullName = "Name",
                    MemberCode = 11011,
                    MemberType =  0,
                    ReferenceId = 1,
                },
                new MemberResponse()
                {
                    Email = "Kaski Nepal",
                    FullName = "Name Name",
                    MemberCode = 11012,
                    MemberType =  0,
                    ReferenceId = 2
                }
            };
            var expected_result = new ServiceResult<List<MemberResponse>>()
            {
                Data = MemberList,
                Message = "Success on retrieving MangerData",
                Status = StatusType.Success

            };
            _memberServiceMock.Setup(x => x.GetMembers());
            //Act
            var actual_result = await _memberManager.GetMembers();
            //Assert
            Assert.Equivalent(expected_result.Status, actual_result.Status);

        }


        [Fact]
        public async Task GetMember_OnSuccess_ReturnMember()
        {
            MemberEntityData.Init();
            // Arrange
            var memberId = 1;
            //var member_response = MemberEntityData.MemberResponse;
            var memberResponse = MemberEntityData.MemberInfo;
            var eMember = MemberEntityData.MemberResponse;

            var expected_result = new ServiceResult<MemberResponse>()
            {
                Data = memberResponse,
                Message = "Member retrieved successfully",
                Status = StatusType.Success
            };
            // Set up the mock behavior of _service.GetMemberById
            _memberServiceMock.Setup(x => x.GetMemberById(memberId)).ReturnsAsync(eMember);

            _mapperMock.Setup(mapper => mapper.Map<MemberResponse>(It.IsAny<EMember>())).Returns(memberResponse);
            //_mapperMock.Setup(x => x.Map<EMember>(It.IsAny<MemberResponse>())).Returns(member_response);

            // Act
            var actual_result = await _memberManager.GetMemberById(memberId);

            // Assert
            Assert.Equal(expected_result.Data, actual_result.Data);
        }



    }
}
