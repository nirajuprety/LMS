using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IBookService
    {
        Task<bool> AddBook(EBook eBook);
        Task<List<EBook>> GetBooks();
        Task<EBook> GetBookByBookID(int id);
        Task<bool> UpdateBookStatus(EBook eBook);
        Task<bool> DeleteBook(int id);
    }
}
