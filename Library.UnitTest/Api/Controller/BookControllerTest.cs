using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using LibraryManagementSystem.Controllers;
using Microsoft.AspNetCore.Http;
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
            _bookController = new BookController(_bookManger.Object);

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
        [Fact]
        public async Task UpdateBook_ReturnsUpdatedBookList()
        {
            //Arrange
            BookSettingDataInfo.Init();
            var IdToUpdate = 1;
            var updatedBookRequest = new BookRequest
            {
                Id = IdToUpdate,
                Title = "Updated Book Title"
            };

            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Book Updated Successfully",
                Status = StatusType.Success
            };
            _bookManger.Setup(service => service.UpdateBook(updatedBookRequest)).ReturnsAsync(expectedResult);

            //Act
            var result = await _bookController.UpdateBook(IdToUpdate, updatedBookRequest) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResult.Message, result.Value);

        }
        [Fact]
        public async Task GetBooksById_ReturnsBook()
        {
            // Arrange
            BookSettingDataInfo.Init();
            int bookId = 1;
            var response = new BookResponse
            {
                Id = 1,
                Title = "Sample Book",
                Author = "XYZ",
                PublicationDate = DateTime.Now,
                ISBN="12345",
                CreatedDate = DateTime.Now,
            };

            var expectedResult = new ServiceResult<BookResponse>
            {
                Data = response
            };
            
            //Act
            _bookManger.Setup(x => x.GetBookById(bookId)).ReturnsAsync(expectedResult);
            var ActualResult = await _bookController.GetBookById(bookId) as OkObjectResult;

            //Assert
            Assert.Equal(expectedResult.Data, ActualResult.Value);
        }

        [Fact]
        public async Task DeleteBooks_OnSuccess_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var expectedResult = new ServiceResult<bool>
            {
                Data = true,
                Message = "Book deleted successfully",
                Status = StatusType.Success,
            };

            //Act
            _bookManger.Setup(x=>x.DeleteBook(id)).ReturnsAsync(expectedResult);
            var ActualResult = await _bookController.DeleteBook(id) as OkObjectResult;

            //Assert
            Assert.Equivalent(expectedResult.Message, ActualResult.Value);
        }
        

    }


}
