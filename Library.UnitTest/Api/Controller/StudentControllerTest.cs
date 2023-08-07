using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using LibraryManagementSystem.Controllers;
using Microsoft.Extensions.Logging;
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
    public class StudentControllerTest
    {
        private readonly StudentController _studentController;

        private readonly Mock<IStudentManager> _studentManagerMock = new Mock<IStudentManager>();
        private readonly Mock<ILogger<StudentController>> _loggerMock = new Mock<ILogger<StudentController>>(); 

        public StudentControllerTest()
        {
            _studentController = new StudentController(_studentManagerMock.Object, _loggerMock.Object);        
        }

        [Fact]
        public async Task AddStudent_OnSuccess_ReturnTrue() 
        {
            //Arrange
            StudentSettingDataInfo.init();
            var request = StudentSettingDataInfo.studentRequest;

            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Student added successfully.",
                Status = StatusType.Success
            };

            //Act
            _studentManagerMock.Setup(x => x.CreateStudent(request)).ReturnsAsync(expectedResult);
            var ActualResult = await _studentController.CreateStudent(request);

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
        public async Task GetStudent_OnSuccess_ReturnResponse()
        {
            //Arrange
            StudentSettingDataInfo.init();
            var response = StudentSettingDataInfo.studentResponseList;

            var expectedResult = new ServiceResult<List<StudentResponse>>
            {
                Data = response,
                Message = "Student List",
                Status = StatusType.Success
            };

            //Act
            _studentManagerMock.Setup(x => x.GetStudents()).ReturnsAsync(expectedResult);
            var ActualResult = await _studentController.GetStudents();

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
        public async Task GetStudentByID_OnSuccess_ReturnResponse()
        {
            //Arrange
            int id = 1;
            StudentSettingDataInfo.init();
            var response = StudentSettingDataInfo.studentResponse;

            var expectedResult = new ServiceResult<StudentResponse>
            {
                Data = response,
                Message = "Student feched",
                Status = StatusType.Success
            };

            //Act
            _studentManagerMock.Setup(x => x.GetStudentByID(id)).ReturnsAsync(expectedResult);
            var ActualResult = await _studentController.GetStudentById(id);

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
        public async Task DeleteStudent_OnSuccess_ReturnTrue()
        {
            //Arrange
            int id = 1;

            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Student deleted successfull",
                Status = StatusType.Success
            };

            //Act
            _studentManagerMock.Setup(x => x.DeleteStudent(id)).ReturnsAsync(expectedResult);
            var ActualResult = await _studentController.DeleteStudent(id);

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
        public async Task UpdateStudent_OnSuccess_ReturnTrue()
        {
            //Arrange
            int id = 1;
            StudentSettingDataInfo.init();
            var request = StudentSettingDataInfo.studentRequest;
            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Student updated successfull",
                Status = StatusType.Success
            };

            //Act
            _studentManagerMock.Setup(x => x.UpdateStudent(request)).ReturnsAsync(expectedResult);
            var ActualResult = await _studentController.UpdateStudent(request);

            //Assert
            Assert.Equivalent(expectedResult, ActualResult);
        }

        [Fact]
		public async Task AddStudent_OnFailure_ReturnFalse()
		{
			//Arrange
			StudentSettingDataInfo.init();
			var request = StudentSettingDataInfo.studentRequest;
            request.Email = "mg.com";

			var expectedResult = new ServiceResult<bool>
			{
				Data = false,
				Message = "Error",
				Status = StatusType.Failure
			};

			//Act
			_studentManagerMock.Setup(x => x.CreateStudent(request)).ReturnsAsync(expectedResult);
			var ActualResult = await _studentController.CreateStudent(request);

			//Assert
			Assert.Equivalent(expectedResult, ActualResult);
		}

		[Fact]
		public async Task GetStudent_OnFailure_ReturnErrorResponse()
		{
			//Arrange
			StudentSettingDataInfo.init();
			var response = StudentSettingDataInfo.studentResponseList;

			var expectedResult = new ServiceResult<List<StudentResponse>>
			{
				Data = response,
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};

			//Act
			_studentManagerMock.Setup(x => x.GetStudents()).ReturnsAsync(expectedResult);
			var ActualResult = await _studentController.GetStudents();

			//Assert
			Assert.Equivalent(expectedResult, ActualResult);
		}

		[Fact]
		public async Task GetStudentByID_OnFailure_ReturnErrorResponse()
		{
			//Arrange
			int id = 1;
			StudentSettingDataInfo.init();
			var response = StudentSettingDataInfo.studentResponse;

			var expectedResult = new ServiceResult<StudentResponse>
			{
				Data = response,
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};

			//Act
			_studentManagerMock.Setup(x => x.GetStudentByID(id)).ReturnsAsync(expectedResult);
			var ActualResult = await _studentController.GetStudentById(id);

			//Assert
			Assert.Equivalent(expectedResult, ActualResult);
		}

		[Fact]
		public async Task DeleteStudent_OnFailure_ReturnFalse()
		{
			//Arrange
			int id = 1;

			var expectedResult = new ServiceResult<bool>
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "User not found"
			};

			//Act
			_studentManagerMock.Setup(x => x.DeleteStudent(id)).ReturnsAsync(expectedResult);
			var ActualResult = await _studentController.DeleteStudent(id);

			//Assert
			Assert.Equivalent(expectedResult, ActualResult);
		}

		[Fact]
		public async Task UpdateStudent_OnFailure_ReturnFalse()
		{
			//Arrange
			StudentSettingDataInfo.init();
			var request = StudentSettingDataInfo.studentRequest;
			var expectedResult = new ServiceResult<bool>
			{
				Data = false,
				Status = StatusType.Failure,
				Message = "Something went wrong"
			};

			//Act
			_studentManagerMock.Setup(x => x.UpdateStudent(request)).ReturnsAsync(expectedResult);
			var ActualResult = await _studentController.UpdateStudent(request);

			//Assert
			Assert.Equivalent(expectedResult, ActualResult);
		}

	}
}
