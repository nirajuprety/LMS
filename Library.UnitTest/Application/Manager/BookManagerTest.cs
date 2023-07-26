using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Application.Manager
{
    public class BookManagerTest
    {
        private readonly BookManager _bookManager;
        private readonly Mock<IBookService> _bookServiceMock = new Mock<IBookService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ILogger<BookManager>> _loggerMock = new Mock<ILogger<BookManager>>();

        public BookManagerTest()
        {
            _bookManager = new BookManager(_bookServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddBook_OnSuccess_ReturnsTrue()
        {
            BookSettingDataInfo.Init();
            // Arrange
            var requestResult = BookSettingDataInfo.SuccessBookSetting;
            var request = BookSettingDataInfo.BookRequest;
            var expecteredResult = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Book Added Succefully",
                Status = StatusType.Success

            };


            _bookServiceMock.Setup(service => service.AddBook(requestResult));

            // Act
            var result = await _bookManager.AddBook(request);

            // Assert
            Assert.Equivalent(expecteredResult.Data, result.Data);

        }


        [Fact]
        public async Task AddBook_OnFailure_ReturnsFalse()
        {
            BookSettingDataInfo.Init();
            // Arrange
            var requestResult = BookSettingDataInfo.SuccessBookSetting;
            var request = BookSettingDataInfo.BookRequest;
            var expectedResult = new ServiceResult<bool>()
            {
                Data = false, // The expected result for failure scenario
                Message = "Failed to add book",
                Status = StatusType.Failure
            };

            _bookServiceMock.Setup(service => service.AddBook(requestResult));

            // Act
            var result = await _bookManager.AddBook(request);

            // Assert
            Assert.Equivalent(expectedResult, result);
        }


        //[Fact]
        //public async Task AddBook_OnSuccess_ReturnsTrue()
        //{
        //    // Arrange
        //    var bookRequest = new BookRequest
        //    {
        //        Title = "Sample Book",
        //        Author = "John Doe",
        //        ISBN = "1234567890",
        //        PublicationDate = DateTime.Now,
        //        UpdatedBy = "User123"
        //    };

        //    _bookServiceMock.Setup(service => service.AddBook(It.IsAny<EBook>())).ReturnsAsync(true);

        //    // Act
        //    var result = await _bookManager.AddBook(bookRequest);

        //    // Assert
        //    Assert.True(result.Data);
        //    Assert.Equal("Book added successfully", result.Message);

        //}



        //[Fact]
        //public async Task AddBook_OnFailure_ReturnsFalse()
        //{
        //    // Arrange
        //    var bookRequest = new BookRequest
        //    {
        //        Title = "Sample Book",
        //        Author = "John Doe",
        //        ISBN = "1234567890",
        //        PublicationDate = DateTime.Now,
        //        UpdatedBy = "User123"
        //    };

        //    _bookServiceMock.Setup(service => service.AddBook(It.IsAny<EBook>())).ReturnsAsync(false);

        //    // Act
        //    var result = await _bookManager.AddBook(bookRequest);

        //    // Assert
        //    Assert.False(result.Data);
        //    Assert.Equal("Failed to add book", result.Message);

        //}


    }
}