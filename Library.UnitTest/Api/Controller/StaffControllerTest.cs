using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
//using Library.UnitTest.Infrastructure.Data;
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
    public class StaffControllerTest
    {

        private readonly StaffController _staffController = null;
        private readonly Mock<IStaffManager> _staffManager = new Mock<IStaffManager>();
        private readonly Mock<ILogger<BookController>> _logger = new Mock<ILogger<BookController>>();

        public StaffControllerTest()
        {
            _staffController = new StaffController(_staffManager.Object);

        }

        [Fact]

        public async Task AddStaff_OnSuccess_ReturnsOkResult()
        {
            //Arrange
            var staffRequest = new StaffRequest()
            {
                Id = 1,
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuye6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffCode = 01,
                StaffType = Domain.Enum.StaffType.Admin
            };
            var expected_result = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Ok",
                Status = StatusType.Success
            };

            //Act
            _staffManager.Setup(x => x.AddStaff(staffRequest)).ReturnsAsync(expected_result);
            var result = await _staffController.AddStaff(staffRequest);

            //Assert
            Assert.Equivalent(expected_result, result);
        }

        [Fact]
        public async Task GetAllStaff_OnSuccess_ReturnDetails()
        {
            //Arrange
            var staffResponse = new List<StaffResponse>()
            {
                new StaffResponse()
                {
                    Id=1,
                Username="SamikshaPhuyel",
                Email="Samikshaphuye6@gmail.com",
                Name="Samiksha",
                Password="Samiksha123!",
                StaffCode=01,
                IsActive=true,
                IsDeleted=false,
                CreatedDate=DateTime.Now,
                UpdatedDate=DateTime.Now,
                StaffType=Domain.Enum.StaffType.Admin
                }

            };

            var expected_result = new ServiceResult<List<StaffResponse>>()
            {
                Data = staffResponse,
                Message = "All Staff found successfully!",
                Status = StatusType.Success,
            };

            //Act
            _staffManager.Setup(x => x.GetAllStaff()).ReturnsAsync(expected_result);
            var actual_result = await _staffController.GetAllStaff();

            //Assert
            Assert.Equivalent(actual_result, expected_result);
        }

        [Fact]
        public async Task GetStaffById_OnSuccess_ReturnDetails()
        {
            //Arrange
            int id = 1;
            var StaffResponse = new StaffResponse()
            {
                Id = id,
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuye6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffCode = 01,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                StaffType = Domain.Enum.StaffType.Admin
            };

            var expected_result = new ServiceResult<StaffResponse>()
            {
                Data = StaffResponse,
                Message = "Staff detail found",
                Status = StatusType.Success,
            };

            //Act
            _staffManager.Setup(x => x.GetStaffById(id)).ReturnsAsync(expected_result);
            var actual_result = await _staffController.GetStaffBbyId(id);


            //Assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]
        public async Task UpdateStaff_OnSuccess_ReturnTrue()
        {
            //Arrange
            var staffRequest = new StaffUpdateRequest()
            {
                Id = 1,
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuye6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffCode = 01,
                StaffType = Domain.Enum.StaffType.Admin
            };

            var expected_result = new ServiceResult<bool>
            {
                Data = true,
                Message = "OK",
                Status = StatusType.Success,
            };

            //Act
            _staffManager.Setup(x => x.UpdateStaff(staffRequest)).ReturnsAsync(expected_result);
            var actual_result = await _staffController.UpdateStaff(staffRequest);

            //Assert
            Assert.Equivalent(actual_result, expected_result);
        }
    }
}



