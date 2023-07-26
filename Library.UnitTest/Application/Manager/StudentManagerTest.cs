using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
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

            // Map the eStudent entity to the StudentRequest DTO
            //var mappedRequest = _mapperMock.Object.Map<StudentRequest>(eStudent);

            _serviceStudentMock.Setup(x => x.CreateStudent(eStudent)).ReturnsAsync(true);
            //Act
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
            var eStudent = StudentSettingDataInfo.eStudent;
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

        //[Fact]
        //public async Task IsValidEmail_OnFailure

    }
}
