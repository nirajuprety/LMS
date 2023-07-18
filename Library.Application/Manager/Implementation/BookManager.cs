using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Manager.Implementation
{
    public class BookManager : IBookManager
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BookManager(IBookService bookService, IMapper mapper)
        {
            _service = bookService;
            _mapper = mapper;
        }

        public async Task<bool> AddBook(BookRequest bookRequest)
        {
            try
            {
                var parse = new EBook()
                {
                    Title = bookRequest.Title,
                    Author = bookRequest.Author,
                    ISBN = bookRequest.ISBN,
                    PublicationDate = bookRequest.PublicationDate,
                };
                var result = await _service.AddBook(parse);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                var book = await _service.GetBookByBookID(id);
                if (book == null)
                {
                    // Book does not exist
                    return false;
                }

                var result = await _service.DeleteBook(id);
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return false;
            }
        }



        public async Task<BookResponse> GetBookById(int id)
        {
            try
            {
                var book = await _service.GetBookByBookID(id);
                var bookResponse = _mapper.Map<BookResponse>(book);
                return bookResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BookResponse>> GetBooks()
        {
            try
            {
                var books = await _service.GetBooks();
                var bookResponses = _mapper.Map<List<BookResponse>>(books);
                return bookResponses;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateBook(BookRequest bookRequest)
        {
            try
            {
                var book = await _service.GetBookByBookID(bookRequest.Id);
                if (book == null)
                    return false;

                book.Title = bookRequest.Title;
                book.Author = bookRequest.Author;
                book.ISBN = bookRequest.ISBN;
                book.PublicationDate = bookRequest.PublicationDate;

                var result = await _service.UpdateBookStatus(book);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
