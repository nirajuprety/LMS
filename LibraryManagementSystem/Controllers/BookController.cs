using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;
using System.Text.Json;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;

        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook(BookRequest bookRequest)
        {
            var result = await _bookManager.AddBook(bookRequest);

            if (result.Status == StatusType.Success)
            {
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
                Log.Information("Book information : {info}", JsonSerializer.Serialize(result.Data));
                return Ok(result.Data);
            }

            return NotFound(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookManager.GetBooks();

            if (result.Status == StatusType.Success)
            {
                Log.Information("Books: {Book}",JsonSerializer.Serialize(result.Data));
                return Ok(result.Data);
            }

            return NotFound(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookRequest bookRequest)
        {
            if (id != bookRequest.Id)
            {
                return BadRequest("Invalid book ID");
            }

            var result = await _bookManager.UpdateBook(bookRequest);

            if (result.Status == StatusType.Success)
            {
                Log.Information("The book has been updated : {updated}", JsonSerializer.Serialize(result.Message));
                return Ok(result.Message);
            }

            return NotFound(result.Message);
        }

    }
}
