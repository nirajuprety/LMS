using Library.Domain.Entities;
using Library.Infrastructure.Repository;
using Library.Infrastructure.Service;
using Library.UnitTest.Application.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTest.Infrastructure.Service
{
    public class BookSettingServiceTest : IClassFixture<DatabaseFixture>
    {

        [Fact]
        public async Task AddBook_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new BookService(factory);
                //arrange
                var requestresult = BookSettingDataInfo.SuccessBookSetting;
                bool EXPECTED_RESULT = true;
                //act
                var result = await service.AddBook(requestresult);
                //assert

                Assert.Equivalent(EXPECTED_RESULT, result);

            }

        }

        [Fact]
        public async Task UpdateBook_OnSuccess_ReturnsData()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new BookService(factory);
                //arrange
                var requestresult = BookSettingDataInfo.SuccessBookSetting;
                bool EXPECTED_RESULT = false;//false passed
                //act
                var result = await service.UpdateBookStatus(requestresult);
                //assert

                Assert.Equal(EXPECTED_RESULT, result);

            }

        }
        [Fact]
        public async Task GetBookById_ExistingBook_ReturnsData()
        {
            //Arrange
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new BookService(factory);
                var bookId = 1;

                //Act
                var result = await service.GetBookByBookID(bookId);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(bookId, result.Id);
                Assert.NotNull(result.Title);

            }
        }
        [Fact]
        public async Task GetBooks_ReturnsListOfBook()
        {
            // Arrange
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new BookService(factory);
                // Act
                var result = await service.GetBooks();

                // Assert
                Assert.NotNull(result);
            }
        }
        [Fact]
        public async Task DeleteBook_SetsIsDeletedToTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new ServiceFactory(_fixture.mockDbContext, true))
            {
                var service = new BookService(factory);

                // Arrange
                int bookIdToDelete = 1; 

                // Act
                await service.DeleteBook(bookIdToDelete);

                // Assert
                
                var deletedBook = _fixture.mockDbContext.Books.FirstOrDefault(b => b.Id == bookIdToDelete);
                Assert.NotNull(deletedBook);
                Assert.True(deletedBook.IsDeleted);
            }
        }



    }
}
