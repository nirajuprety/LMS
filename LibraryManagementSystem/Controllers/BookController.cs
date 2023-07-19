using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async Task<IActionResult> AddBook(BookRequest bookRequest)
        {
            var result = await _bookManager.AddBook(bookRequest);

            if (result.Status == StatusType.Success)
            {
                return Ok(result.Message);
            }

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

            return NotFound(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookManager.GetBookById(id);

            if (result.Status == StatusType.Success)
            {
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
                return Ok(result.Message);
            }

            return NotFound(result.Message);
        }
        [HttpPost("{bookId}/borrow")]
        [Authorize(Roles = "staff")] 
        public async Task<IActionResult> BorrowBook(int bookId, int memberId)
        {

            var result = await _bookManager.BorrowBook(bookId, memberId);

            if (!result)
            {
                return StatusCode(500, new { Error = "Cannot borrow an inactive book or book not found." });
            }

            return Ok(new { Message = $"Book borrowed successfully by Id {memberId}." });
        }
    }
}
