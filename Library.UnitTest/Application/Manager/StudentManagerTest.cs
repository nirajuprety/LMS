using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using LibraryManagementSystem.Controllers;
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
    public class StudentManagerTest
    {
        private readonly StudentManager _studentManager;

        private readonly Mock<IStudentService> _serviceStudentMock = new Mock<IStudentService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IMemberService> _serviceMemberMock = new Mock<IMemberService>();

        public StudentManagerTest()
        {
            _studentManager = new StudentManager(_serviceStudentMock.Object,
                _mapperMock.Object, _serviceMemberMock.Object);
        }

        [Fact]
        public async Task AddStudent_OnSucess_ReturnTrue()
        {
            //Arrange
            StudentSettingDataInfo.init();
            var request = StudentSettingDataInfo.studentRequest;

            var eStudent = StudentSettingDataInfo.eStudent;
            var expectedResult = new ServiceResult<bool>()
            {
                Data = true,
                Message = "User Created Successfull",
                Status = StatusType.Success
            };

            //Act
            // Map the eStudent entity to the StudentRequest DTO
            _mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
            _serviceStudentMock.Setup(x => x.CreateStudent(eStudent)).ReturnsAsync(true);
            var actualResult = await _studentManager.CreateStudent(request);

            //Assert
            Assert.Equivalent(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetStudent_OnSuccess_ReturnResponse()
        {
            //Arrange
            StudentSettingDataInfo.init();
            var eStudent = StudentSettingDataInfo.eStudentList;
            var response = StudentSettingDataInfo.studentResponseList;
            var expectedResult = new ServiceResult<List<StudentResponse>>()
            {
                Data = response,
                Status = StatusType.Success,
                Message = "Users List"
            };
            _serviceStudentMock.Setup(x => x.GetStudents()).ReturnsAsync(eStudent);
            //Act
            var actualResult = await _studentManager.GetStudents();
            //Assert
            Assert.Equivalent(expectedResult, actualResult);
        }

		[Fact]
        public async Task GetStudentById_OnSuccess_ReturnResponse()
        {
            //Arrange
            int id = 1;
            StudentSettingDataInfo.init();
            var eStudent = StudentSettingDataInfo.eStudentWithID;
            var response = StudentSettingDataInfo.studentResponse;
            var expectedResult = new ServiceResult<StudentResponse>()
            {
                Data = response,
                Status = StatusType.Success,
                Message = "User Found Successfull"
            };
            _serviceStudentMock.Setup(x => x.GetStudentByID(id)).ReturnsAsync(eStudent);
            //Act
            var actualResult = await _studentManager.GetStudentByID(id);
            //Assert
            Assert.Equivalent(expectedResult, actualResult);
        }

		[Fact]
        public async Task DeleteStudent_OnSuccess_ReturnTrue()
        {
            //Arrange
            int id = 1;
            var expectedResult = new ServiceResult<bool>()
            {
                Data = true,
                Status = StatusType.Success,
                Message = "User Delete Successfull"
            };
            _serviceStudentMock.Setup(x => x.DeleteStudent(id)).ReturnsAsync(true);
            //Act
            var actualResult = await _studentManager.DeleteStudent(id);
            //Assert
            Assert.Equivalent(expectedResult, actualResult);
        }

		[Fact]
        public async Task UpdateStudent_OnSuccess_ReturnTrue()
        {
            //Arrange
            int id = 1;

            StudentSettingDataInfo.init();
            var eRequest = StudentSettingDataInfo.eStudent;
            var request = StudentSettingDataInfo.studentRequest;
            var expectedResult = new ServiceResult<bool>()
            {
                Data = true,
                Status = StatusType.Success,
                Message = "Student updated successfull"
            };
            _serviceStudentMock.Setup(x => x.UpdateStudent(It.IsAny<EStudent>())).ReturnsAsync(true);
            //Act
            var actualResult = await _studentManager.UpdateStudent(request);
            //Assert
            Assert.Equivalent(expectedResult, actualResult);
        }


        [Fact]
        public async Task AddStudentShouldAddMember_OnSuccess_ReturnTrue()
        {
            //Arrange
            StudentSettingDataInfo.init();
            var request = StudentSettingDataInfo.studentRequest;
            var eStudent = StudentSettingDataInfo.eStudent;
            var eMember = StudentSettingDataInfo.eMember;

            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "User Created Successfull",
                Status = StatusType.Success
            };

            //
            _mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
            _serviceMemberMock.Setup(x => x.CreateMember(eMember)).ReturnsAsync(eMember.Id);
            var ActualResult = await _studentManager.CreateStudent(request);

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
        public async Task IsValidEmail_OnFailure_ReturnFalse()
        {
            //Arrange
            var request = new StudentRequest() {Email = "mG.com" };
            var eStudent = StudentSettingDataInfo.eStudent;
            eStudent.Email = request.Email;
            var Expected_Result = new ServiceResult<bool>()
            {
                Data = false,
                Status = StatusType.Failure,
                Message = "Email need to have special character."
            };
            //Act
            _mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
            var Actual_Result = await _studentManager.CreateStudent(request);
            //Assert
            Assert.Equivalent(Expected_Result, Actual_Result);
        }

        [Fact]
        public async Task IsUniqueEmail_OnFailure_ReturnFalse()
        {
            //Arrange
            var request = new StudentRequest() { Email = "m@g.com" };
            var eStudent = StudentSettingDataInfo.eStudent;
            eStudent.Email = request.Email;
            var Expected_Result = new ServiceResult<bool>()
            {
                Data = false,
                Status = StatusType.Failure,
                Message = "Enter Unique Email"
            };
            //Act
            _mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
            _serviceStudentMock.Setup(x => x.IsUniqueEmail(request.Email)).ReturnsAsync(true);
            var Actual_Result = await _studentManager.CreateStudent(request);
            //Assert
            Assert.Equivalent(Expected_Result, Actual_Result);
        }

        [Fact]
        public async Task CreateMemberWhileStudentCreation_OnSuccess_ReturnTrue()
        {
            //Arrange
            StudentSettingDataInfo.init();
            var eStudent = StudentSettingDataInfo.eStudent;
            var studentRequest = StudentSettingDataInfo.studentRequest;
            var eMember = StudentSettingDataInfo.eMember;
            var Expected_Result = new ServiceResult<bool>()
            {
                Data = true,
                Status = StatusType.Success,
                Message = "User Created Successfull"
            };
            //Act

            _mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
            _serviceMemberMock.Setup(x => x.CreateMember(eMember)).ReturnsAsync(eMember.Id);
            var Actual_Result = await _studentManager.CreateStudent(studentRequest);
            //Assert
            Assert.Equivalent(Expected_Result, Actual_Result);
        }


		[Fact]
		public async Task AddStudent_OnFailure_ReturnFalse()
		{
			//Arrange
			StudentSettingDataInfo.init();
			var request = StudentSettingDataInfo.studentRequest;
			var eStudent = StudentSettingDataInfo.eStudent;
			var ExpectedResult = new ServiceResult<bool>()
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};
			_mapperMock.Setup(x => x.Map<EStudent>(It.IsAny<StudentRequest>())).Returns(eStudent);
			_serviceStudentMock.Setup(x => x.IsUniqueEmail(request.Email)).ReturnsAsync(false);
			_serviceStudentMock.Setup(x => x.CreateStudent(eStudent)).ThrowsAsync(new Exception());

			//Act
			var Actual_Result = await _studentManager.CreateStudent(request);
			//Assert
			Assert.Equivalent(ExpectedResult, Actual_Result);
		}
         

		[Fact]
		public async Task GetStudent_OnException_ReturnExceptionResponse()
		{
			//Arrange
			var Expected_Result = new ServiceResult<List<StudentResponse>>()
			{
				Data = new List<StudentResponse>(),
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};
			_serviceStudentMock.Setup(x => x.GetStudents()).ThrowsAsync(new Exception());
			//Act
			var Actual_Result = await _studentManager.GetStudents();
			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task GetStudentByID_OnException_ReturnExceptionResponse()
		{
			//Arrange
			int id = 1;
			StudentSettingDataInfo.init();
			var eStudent = StudentSettingDataInfo.eStudent;
			var Expected_Result = new ServiceResult<StudentResponse>()
			{
				Data = new StudentResponse(),
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};
			_serviceStudentMock.Setup(x => x.GetStudentByID(id)).ThrowsAsync(new Exception());
			//Act
			var Actual_Result = await _studentManager.GetStudentByID(id);
			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task DeleteStudent_OnFailure_ReturnFalse_WithUserNotFoundMsg()
		{
			//Arrange
			int id = 1;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "User not found"
			};
			_serviceStudentMock.Setup(x => x.DeleteStudent(id)).ReturnsAsync(false);
			//Act
			var Actual_Result = await _studentManager.DeleteStudent(id);
			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task DeleteStudent_OnFailure_ReturnFalse()
		{
			//Arrange
			int id = 1;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};
			_serviceStudentMock.Setup(x => x.DeleteStudent(id)).ThrowsAsync(new Exception());
			//Act
			var Actual_Result = await _studentManager.DeleteStudent(id);
			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}

		[Fact]
		public async Task UpdateStudent_OnFailure_ReturnFalse()
		{
            //Arrange
            StudentSettingDataInfo.init();
            var request = StudentSettingDataInfo.studentRequest;
            var eStudent = StudentSettingDataInfo.eStudent;
			var Expected_Result = new ServiceResult<bool>()
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "Student not found"
			};
			_serviceStudentMock.Setup(x => x.UpdateStudent(eStudent)).ReturnsAsync(false);
			//Act
			var Actual_Result = await _studentManager.UpdateStudent(request);
			//Assert
			Assert.Equivalent(Expected_Result, Actual_Result);
		}
	}
}
