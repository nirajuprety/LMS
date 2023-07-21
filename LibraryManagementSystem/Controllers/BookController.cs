using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Data;
using System.Text.Json;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookManager bookManager, ILogger<BookController> logger)
        {
            _bookManager = bookManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookRequest bookRequest)
        {
            var result = await _bookManager.AddBook(bookRequest);

            if (result.Status == StatusType.Success)
            {
                Log.Information("Book added successfully : {Book}", JsonSerializer.Serialize(bookRequest));
                return Ok(result.Message);
            }
            Log.Error("Error adding book: {ErrorMessage}", JsonSerializer.Serialize(result.Message));

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookManager.DeleteBook(id);

            if (result.Status == StatusType.Success)
            {
                Log.Information(result.Message);
                return Ok(result.Message);
                
            }
            Log.Error(result.Message);
            return NotFound(result.Message); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookManager.GetBookById(id);

            if (result.Status == StatusType.Success)
            {
                Log.Information($"{result.Data}");
                return Ok(result.Data);
            }
            Log.Warning(result.Message);
            return NotFound(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookManager.GetBooks();

            if (result.Status == StatusType.Success)
            {
                Log.Information($"Books: {result.Data}");
                return Ok(result.Data);
            }
            Log.Information (result.Message);
            return NotFound(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookRequest bookRequest)
        {
            if (id != bookRequest.Id)
            {
                Log.Warning("Bad request-Invalid ID");
                return BadRequest("Invalid book ID");
            }

            var result = await _bookManager.UpdateBook(bookRequest);

            if (result.Status == StatusType.Success)
            {
                Log.Information(result.Message);
                return Ok(result.Message);
            }
            Log.Warning(result.Message);
            return NotFound(result.Message);
        }

        [HttpPost("{bookId}/borrow")]
        //[Authorize(Roles = "staff")] 
        public async Task<IActionResult> BorrowBook(int bookId, int memberId)
        {

            var result = await _bookManager.BorrowBook(bookId, memberId);

            if (result.Status == StatusType.Failure)
            {
                return StatusCode(500, new { Error = "Cannot borrow an inactive book or book not found." });
            }

            return Ok(new { Message = $"Book borrowed successfully by Id {memberId}." });
        }
    }
}
