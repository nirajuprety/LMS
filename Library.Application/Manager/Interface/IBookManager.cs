using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Interface
{
    public interface IBookManager
    {
        Task<ServiceResult<bool>> AddBook(BookRequest bookRequest);
        Task<ServiceResult<bool>> DeleteBook(int id);
        Task<ServiceResult<BookResponse>> GetBookById(int id);
        Task<ServiceResult<List<BookResponse>>> GetBooks();
        Task<ServiceResult<bool>> UpdateBook(BookRequest bookRequest);
        Task<ServiceResult<bool>> BorrowBook(int bookId, int memberId);
    }
}
