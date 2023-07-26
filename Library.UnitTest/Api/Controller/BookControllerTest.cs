﻿using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
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
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResult.Message, result.Value);
        }
        [Fact]
        public async Task AddBook_Failure_ReturnsBadRequestResult()
        {
            BookSettingDataInfo.Init();
            // Arrange
            var bookRequest = BookSettingDataInfo.BookRequest;

            var expectedResult = new ServiceResult<bool>
            {
                Data = false,
                Message = "Failed to add the book.",
                Status = StatusType.Failure
            };

            _bookManger.Setup(service => service.AddBook(bookRequest)).ReturnsAsync(expectedResult);

            // Act
            var result = await _bookController.AddBook(bookRequest) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(expectedResult.Message, result.Value);
        }
        [Fact]
        public async Task GetAllBooks_ReturnsOkResultWithBookList()
        {
            // Arrange
            var expectedBooks = new List<BookResponse>
            {
                new BookResponse { Id = 1, Title = "Book 1" },
                new BookResponse { Id = 2, Title = "Book 2" },

            };

            var expectedResult = new ServiceResult<List<BookResponse>>
            {
                Data = expectedBooks,
                Message = "Books retrieved successfully.",
                Status = StatusType.Success
            };

            _bookManger.Setup(service => service.GetBooks()).ReturnsAsync(expectedResult);

            // Act
            var result = await _bookController.GetBooks() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var bookList = Assert.IsAssignableFrom<List<BookResponse>>(result.Value);
            Assert.Equal(expectedBooks.Count, bookList.Count);

        }






    }
}