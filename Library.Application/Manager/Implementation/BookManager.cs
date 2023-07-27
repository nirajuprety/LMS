using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{
    public class BookManager : IBookManager
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<BookManager> _logger;


        public BookManager(IBookService bookService, IMapper mapper, ILogger<BookManager> logger)
        {
            _service = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> AddBook(BookRequest bookRequest)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var parse = new EBook()
                {
                    Id=bookRequest.Id,
                    Title = bookRequest.Title,
                    Author = bookRequest.Author,
                    ISBN = bookRequest.ISBN,
                    PublicationDate = bookRequest.PublicationDate,
                    UpdatedBy = bookRequest.UpdatedBy,
                    BookStatus=Domain.Enum.BookStatus.Available
                };

                var result = await _service.AddBook(parse);
                _logger.LogInformation("Book added Successfully" + JsonConvert.SerializeObject(parse));

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Book added successfully" : "Failed to add book";
                serviceResult.Data = result;
                return serviceResult;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Something Went wrong");
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while adding the book";
                serviceResult.Data = false;
                return serviceResult;
            }
        }

        public async Task<ServiceResult<bool>> BorrowBook(int bookId, int memberId)
        {
            var serviceResult = new ServiceResult<bool>();
            bool isBorrowed = await _service.BorrowBook(bookId, memberId);
            if (isBorrowed)
            {
                var book = await _service.GetBookByBookID(bookId);
                if (book != null)
                {
                    serviceResult.Data = true;
                    serviceResult.Status = StatusType.Success;
                    serviceResult.Message = $"Book '{book.Title} added successfully";
                }
                else
                {
                    serviceResult.Data = false;
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book not found";
                }

            }
            else
            {
                serviceResult.Data = false;
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "Book borrowing failed.";
            }
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteBook(int id)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var book = await _service.GetBookByBookID(id);
                if (book == null)
                {
                    // Book does not exist
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }

                var result = await _service.DeleteBook(id);

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Book deleted successfully" : "Failed to delete book";
                serviceResult.Data = result;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while deleting the book";
                serviceResult.Data = false;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<BookResponse>> GetBookById(int id)
        {
            var serviceResult = new ServiceResult<BookResponse>();

            try
            {
                var book = await _service.GetBookByBookID(id);
                if (book == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book not found";
                    serviceResult.Data = null;

                    return serviceResult;
                }

                var bookResponse = _mapper.Map<BookResponse>(book);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Book retrieved successfully";
                serviceResult.Data = bookResponse;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the book";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<List<BookResponse>>> GetBooks()
        {
            var serviceResult = new ServiceResult<List<BookResponse>>();

            try
            {
                var books = await _service.GetBooks();
                var bookResponses = _mapper.Map<List<BookResponse>>(books);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Books retrieved successfully";
                serviceResult.Data = bookResponses;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the books";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<bool>> UpdateBook(BookRequest bookRequest)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var book = await _service.GetBookByBookID(bookRequest.Id);
                if (book == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }

                book.Title = bookRequest.Title;
                book.Author = bookRequest.Author;
                book.ISBN = bookRequest.ISBN;
                book.PublicationDate = bookRequest.PublicationDate;

                var result = await _service.UpdateBookStatus(book);

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Book updated successfully" : "Failed to update book";
                serviceResult.Data = result;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while updating the book";
                serviceResult.Data = false;

                return serviceResult;
            }
        }
    }
}