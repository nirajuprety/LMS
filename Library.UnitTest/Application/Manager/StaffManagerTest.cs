 using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using LibraryManagementSystem.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Application.Manager
{
    public class StaffManagerTest
    {
        private readonly StaffManager _staffManager;
        private readonly Mock<IStaffService> _staffServiceMock = new Mock<IStaffService>();
        private readonly Mock<IMemberService> _memberServiceMock = new Mock<IMemberService>();
        private readonly Mock<ILoginService> _loginServiceMock = new Mock<ILoginService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        // private readonly Mock<ILogger<StaffManager>> _loggerMock = new Mock<ILogger<StaffManager>>();


        public StaffManagerTest()
        {
            _staffManager = new StaffManager(_staffServiceMock.Object, _mapperMock.Object, _loginServiceMock.Object, _memberServiceMock.Object);
        }

        [Fact]
        public async Task AddStaff_IsEmailValid_ShouldReturnFailureResult()
        {
            //Arranage
            var StaffRequest = new StaffRequest()
            {
                Email = "Samikshaphuyelgmailcom",
            };

            var expected_result = new ServiceResult<bool>()
            {
                Data = false,
                Message = "Email isnot valid",
                Status = StatusType.Failure
            };

            //Act 
            var result = await _staffManager.AddStaff(StaffRequest);

            //Assert
            Assert.Equivalent(expected_result.Data, result.Data);
        }

        [Fact]
        public async Task AddStaff_IsPasswordValid_ShouldReturnFailureResult()

        {
            //Arrange
            var staffRequest = new StaffRequest()
            {
                Password = "password",
                Email = "Samikshaphuyel@gmail.com"

            };
            var expected_result = new ServiceResult<bool>()
            {
                Data = false,
                Message = "Password isnot valid",
                Status = StatusType.Failure
            };

            //Act

            var result = await _staffManager.AddStaff(staffRequest);

            //Assert
            Assert.Equivalent(result.Data, expected_result.Data);
        }

        [Fact]

        public async Task AddStaff_IsStaffCodeExist_ReturnsfailureResult()
        {
            //Arrange
            var staffRequest = new StaffRequest()
            {

                Password = "password123!!!",
                Email = "Samikshaphuyel@gmail.com",
                StaffCode = 001
            };

            var eStaff = new EStaff()
            {
                Password = staffRequest.Password,
                Email = staffRequest.Email,
                StaffCode = staffRequest.StaffCode
            };

            var expected_result = new ServiceResult<bool>()
            {
                Data = false,
                Message = "The provided StaffCode already exists in the system",
                Status = StatusType.Failure
            };

            //Act
            _staffServiceMock.Setup(x => x.IsUniqueStaffCode(eStaff.StaffCode)).ReturnsAsync(true);

            var actual_result = await _staffManager.AddStaff(staffRequest);

            //Assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]

        public async Task HashedPassword_ValidPassword_ReturnsCorrectHash()
        {
            //Arrange
            var staffRequest = new StaffRequest()
            {

                Password = "password123!!!",
                Email = "Samikshaphuyel@gmail.com",

            };

            var expected_result = "eljAwa9vhpbc9udLM/hyBF7Was3s4ILv0jGgvEng3S8=";

            //Act
            var actual_result = _staffManager.HashPassword(staffRequest.Password);

            //Assert
            Assert.Equivalent(expected_result, actual_result);

        }


        [Fact]
        public async Task AddStaff_IsValidStaffType_ReturnsFailureResult()
        {
            var staffRequest = new StaffRequest()
            {
                Password = "password123!!!",
                Email = "Samikshaphuyel@gmail.com",
                StaffCode = 001,
                StaffType = (StaffType)2
            };
            var eStaff = new EStaff()
            {
                Password = staffRequest.Password,
                Email = staffRequest.Email,
                StaffCode = staffRequest.StaffCode,
                StaffType = staffRequest.StaffType
            };


            var expected_result = new ServiceResult<bool>()
            {
                Data = false,
                Message = "Invalid StaffType value. StaffType can only be 0 (admin) or 1 (staff).",
                Status = StatusType.Failure
            };

            //act
            _staffServiceMock.Setup(x => x.AddStaff(eStaff)).ReturnsAsync(0);

            var actual_result = await _staffManager.AddStaff(staffRequest);

            //assert
            Assert.Equivalent(expected_result, actual_result);

        }

        [Fact]
        public async Task AddStaff_OnSuccess_ReturnsTrue()
        {

            //arrange
            StaffDataInfo.Init();
            var request = StaffDataInfo.StaffRequest;
            var eStaff = StaffDataInfo.eStaffRequest;
            var hashedPassword = _staffManager.HashPassword(request.Password);
            var ELogin = StaffDataInfo.LoginInfo;
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Added successfully",
                Status = StatusType.Success
            };

            _mapperMock.Setup(mapper => mapper.Map<EStaff>(It.IsAny<StaffRequest>())).Returns(eStaff);

            //Act
            _staffServiceMock.Setup(x => x.AddStaff(eStaff)).ReturnsAsync(eStaff.Id);
            _staffServiceMock.Setup(x => x.CreateLogin(It.IsAny<ELogin>())).ReturnsAsync(true);

            var actual_result = await _staffManager.AddStaff(request);

            //Assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]
        public async Task AddStaff_OnFailure_ReturnsFailureResult()
        {
            //Arrange
            StaffDataInfo.Init();
            var eRequest = StaffDataInfo.eStaffRequest;
            var request = StaffDataInfo.StaffRequest;
            var response = StaffDataInfo.eStaffResponse;
            var expected_result = new ServiceResult<bool>()
            {
                Data = false,
                Message = "Something went wrong",
                Status = StatusType.Failure
            };
            _mapperMock.Setup(mapper => mapper.Map<EStaff>(It.IsAny<StaffRequest>())).Returns(response);
            _staffServiceMock.Setup(x => x.AddStaff(It.IsAny<EStaff>())).ThrowsAsync(new Exception());
            //_recipientService.Setup(x => x.AddBankRecipient(It.IsAny<ERecipient>())).ThrowsAsync(new Exception("Simulated exception"));

            //act
            var actual_result = await _staffManager.AddStaff(request);

            //assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]
        public async Task GetAllStaff_OnSuccess_ReturnDetails()
        {
            //arrange
            StaffDataInfo.Init();
            var response = StaffDataInfo.StaffResponseList;
            var eStaff = StaffDataInfo.eStaffList;
            var expected_result = new ServiceResult<List<StaffResponse>>()
            {
                Data = response,
                Message = "All Staff found successfully!",
                Status = StatusType.Success
            };

            //Act
            _staffServiceMock.Setup(x => x.GetAllStaff()).ReturnsAsync(eStaff);
            var actual_result = await _staffManager.GetAllStaff();

            //Assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]
        public async Task GetStaffById_OnSuccess_ReturnStaffDetail()
        {
            //arrange
            int id = 1;
            StaffDataInfo.Init();
            var response = StaffDataInfo.StaffResponse;
            var eStaff = StaffDataInfo.eStaffResponse;
            var expected_result = new ServiceResult<StaffResponse>()
            {
                Data = response,
                Message = "Staff found successfully!",
                Status = StatusType.Success,
            };

            //act
            _staffServiceMock.Setup(x => x.GetStaffById(id)).ReturnsAsync(eStaff);
            var actual_result = await _staffManager.GetStaffById(id);
            //assert
            Assert.Equivalent(expected_result, actual_result);

        }

        [Fact]
        public async Task UpdateStaff_OnSuccess_ReturnsTrue()
        
        {
            //arrange
            StaffDataInfo.Init();
            var request = StaffDataInfo.StaffUpdateRequest;
            var eStaffResponse = StaffDataInfo.eStaffResponse;
            var eStaffRequest=StaffDataInfo.eStaffRequest;

            var eMember = StaffDataInfo.eMember;
            var eLogin = StaffDataInfo.eLogin;

            var expected_result = new ServiceResult<bool>
            {
                Data = true,
                Message = "Staff Updated Successfully!",
                Status = StatusType.Success,
            };

            //act
            _staffServiceMock.Setup(x => x.GetStaffById(request.Id)).ReturnsAsync(eStaffResponse);
            _mapperMock.Setup(mapper => mapper.Map<EStaff>(It.IsAny<StaffUpdateRequest>())).Returns(eStaffResponse);
            _staffServiceMock.Setup(x => x.UpdateStaff(It.IsAny<EStaff>())).ReturnsAsync(true);
            _mapperMock.Setup(mapper => mapper.Map<EMember>(It.IsAny<StaffUpdateRequest>())).Returns(eMember);
            _memberServiceMock.Setup(x => x.UpdateMember(It.IsAny<EMember>())).ReturnsAsync(true);
            _mapperMock.Setup(mapper => mapper.Map<ELogin>(It.IsAny<StaffUpdateRequest>())).Returns(eLogin);

            _staffServiceMock.Setup(x => x.UpdateUser(It.IsAny<ELogin>())).ReturnsAsync(true);
            var actual_result = await _staffManager.UpdateStaff(request);

            //assert
            Assert.Equivalent(actual_result, expected_result);
        }


        [Fact]

        public async Task DeleteStaff_OnSuccess_ReturnsTrue()
        {
            //arrange
            int id = 1;

            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Staff deleted successfully!",
                Status = StatusType.Success
            };

            //act
            _staffServiceMock.Setup(x => x.DeleteStaff(id)).ReturnsAsync(expected_result.Data);
            var actual_result = await _staffManager.DeleteStaff(id);

            //Assert
            Assert.Equivalent(expected_result, actual_result);
        }
    }
}

