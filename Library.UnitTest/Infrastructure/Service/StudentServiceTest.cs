using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Library.UnitTest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest.Infrastructure.Service
{
    public class StudentServiceTest: IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task AddStudent_OnSuccess_ReturnTrue()
        { 
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            { 
                var service = new StudentService(factory);
                //Arrange
                StudentSettingDataInfo.init();
                var request = StudentSettingDataInfo.eStudent;
                var expectedResult = true;
                //Act
                var result = await service.CreateStudent(request);
                //Assert
                Assert.Equivalent(expectedResult, result);  
            }
        }

        //Revisit
        [Fact]
        public async Task GetStudent_OnSuccess_ReturnEStudentList()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StudentService(factory);
                //Arrange
                StudentSettingDataInfo.init();
                var expectedResult = StudentSettingDataInfo.eStudent; //Need to change eStudentList

                //Act
                var result = await service.GetStudents();

                //Assert
                Assert.Equivalent(expectedResult, result);
            }
        }

        [Fact]
        public async Task GetStudentByID_OnSuccess_ReturnEStudentList()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StudentService(factory);
                //Arrange
                int id = 1;
                StudentSettingDataInfo.init();
                var eStudent = StudentSettingDataInfo.eStudent;

                var expectedResult = eStudent;
                //Act
                var Actual_Result = await service.GetStudentByID(id);

                //Assert
                Assert.Equivalent(expectedResult, Actual_Result);
            }
        }

        [Fact]
        public async Task UpdateStudent_OnSuccess_ReturnTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StudentService(factory);
                //Arrange
                StudentSettingDataInfo.init();
                var eStudent = StudentSettingDataInfo.eStudent;

                var expectedResult = true;
                //Act
                var Actual_Result = await service.UpdateStudent(eStudent);

                //Assert
                Assert.Equivalent(expectedResult, Actual_Result);
            }
        }

        [Fact]
        public async Task DeleteStudent_OnSuccess_ReturnTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new StudentService(factory);
                //Arrange
                StudentSettingDataInfo.init();
                var eStudent = StudentSettingDataInfo.eStudent;

                var expectedResult = true;
                //Act
                var Actual_Result = await service.DeleteStudent(eStudent.Id);

                //Assert
                Assert.Equivalent(expectedResult, Actual_Result);

            }
        }
    }
}
