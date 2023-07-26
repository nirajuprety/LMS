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
            _bookManager = new BookManager(_bookServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
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


            _bookServiceMock
                .Setup(service => service.AddBook(requestResult));

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

            _bookServiceMock
                .Setup(service => service.AddBook(requestResult));

            // Act
            var result = await _bookManager.AddBook(request);

            // Assert
            Assert.Equivalent(expectedResult, result);
        }
        [Fact]
        public async Task GetBookById_ExistingBook_ReturnsBookResponse()
        {
            // Arrange
            var bookId = 1;
            var expectedBook = new EBook { Id = bookId, Title = "Sample Book" };
            var expectedBookResponse = new BookResponse { Id = bookId, Title = "Sample Book" };

            _bookServiceMock
                .Setup(service => service.GetBookByBookID(bookId))
                .ReturnsAsync(expectedBook);
            _mapperMock
                .Setup(mapper => mapper.Map<BookResponse>(expectedBook))
                .Returns(expectedBookResponse);

            // Act
            var result = await _bookManager.GetBookById(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.Success, result.Status);
            Assert.Equal(expectedBookResponse, result.Data);
        }
        [Fact]
        public async Task UpdateBook_OnSuccess_ReturnsTrue()
        {
            // Arrange
            var request = new BookRequest { Id = 1, Title = "Updated Book" };
            var expectedBook = new EBook { Id = 1, Title = "Sample Book" };

            _bookServiceMock
                .Setup(service => service.GetBookByBookID(request.Id))
                .ReturnsAsync(expectedBook);
            _mapperMock.
                Setup(mapper => mapper.Map(request, expectedBook))
                .Returns(expectedBook);
            _bookServiceMock
                .Setup(service => service.UpdateBookStatus(expectedBook))
                .ReturnsAsync(true);

            // Act
            var result = await _bookManager.UpdateBook(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.Success, result.Status);
            Assert.True(result.Data);
        }
        [Fact]
        public async Task GetAllBooks_ReturnsListOfBookResponses()
        {
            // Arrange
            var expectedBooks = new List<EBook>
            {
                new EBook { Id = 1, Title = "Book 1" },
                new EBook { Id = 2, Title = "Book 2" }
            };
            var expectedBookResponses = expectedBooks.Select(book => new BookResponse { Id = book.Id, Title = book.Title }).ToList();

            _bookServiceMock
                .Setup(service => service.GetBooks())
                .ReturnsAsync(expectedBooks);
            _mapperMock
                .Setup(mapper => mapper.Map<List<BookResponse>>(expectedBooks))
                .Returns(expectedBookResponses);

            // Act
            var result = await _bookManager.GetBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusType.Success, result.Status);
            Assert.Equal(expectedBookResponses, result.Data);
        }
    }
}