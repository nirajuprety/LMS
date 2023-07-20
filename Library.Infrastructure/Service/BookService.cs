using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class BookService : IBookService
    {
        private readonly IServiceFactory _factory;
        private readonly IConfiguration _configuration;

        public BookService(IServiceFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }
        public async Task<bool> AddBook(EBook eBook)
        {
            try
            {
                var service = _factory.GetInstance<EBook>();
                await service.AddAsync(eBook);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> BorrowBook(int id, int memberId)
        {
            var bookservice = _factory.GetInstance<EBook>();
            var memberService = _factory.GetInstance<EMember>();
            var book = await bookservice.FindAsync(id);
            if (book == null || !book.IsActive) 
            {
                return false;
            }

            var member = await memberService.FindAsync(id);
            if (member == null) {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                var service = _factory.GetInstance<EBook>();
                var user = await service.FindAsync(id);
                if (user == null)
                {
                    return false;
                }
                await service.RemoveAsync(user);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<EBook> GetBookByBookID(int id)
        {
            try
            {
                var service = _factory.GetInstance<EBook>();
                var user = await service.FindAsync(id);
                if (user == null )
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<List<EBook>> GetBooks()
        {
            try
            {
                var service = _factory.GetInstance<EBook>();
                var result = await service.ListAsync();
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

       
        public async Task<bool> UpdateBookStatus(EBook eBook)
        {
            try
            {
                var service = _factory.GetInstance<EBook>();
                var user = await service.FindAsync(eBook.Id);
                if (user == null)
                {
                    return false;
                }
                user.Title = eBook.Title;
                user.Author = eBook.Author;
                user.ISBN = eBook.ISBN;
                user.PublicationDate = eBook.PublicationDate;
                user.UpdatedBy = eBook.UpdatedBy;
                await service.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
