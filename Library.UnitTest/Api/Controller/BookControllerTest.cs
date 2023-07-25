using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using LibraryManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
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
    public class BookSettingControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly BookController _bookController = null;
        private readonly Mock<IBookManager> _bookManger = new Mock<IBookManager>();
        private readonly Mock<ILogger<BookController>> _logger = new Mock<ILogger<BookController>>();
        public BookSettingControllerTest()
        {
            _bookController = new BookController(_bookManger.Object, _logger.Object);

        }


        [Fact]
        public async Task AddBook_Success_ReturnsOkResult()
        {
            BookSettingDataInfo.Init();
            // Arrange
            var bookRequest = BookSettingDataInfo.BookRequest;

            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Book added successfully.",
                Status = StatusType.Success
            };

            _bookManger.Setup(service => service.AddBook(bookRequest)).ReturnsAsync(expectedResult);

            // Act
            var result = await _bookController.AddBook(bookRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode); // Ensure it's an OkResult
            Assert.Equal(expectedResult.Message, result.Value); // Verify the message in the response
        }


    }
}