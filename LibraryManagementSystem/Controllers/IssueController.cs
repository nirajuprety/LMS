using Library.Application.DTO.Request;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using System.Text.Json;
using static Library.Infrastructure.Service.Common;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly IIssueManager _issueManager;

        public IssueController(IIssueManager issueManager)
        {
            _issueManager = issueManager;
        }
        //[Authorize]
        [HttpPost]
        //public async Task<IActionResult> AddIssue(IssueRequest issueRequest)
        public async Task<ServiceResult<bool>> AddIssue(IssueRequest issueRequest)
        {
            //get the user id using JWT token
            //var userId = User.FindFirstValue("UserId");
            var userID = "1";
            Log.Information("User ID of the person who issued the book: {UserId}", userID);
            var result = await _issueManager.AddIssue(issueRequest, userID);

            if (result.Status == StatusType.Success)
            {
                //return Ok(result.Message);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Message = "Ok",
                    Status = StatusType.Success
                };
            }
            Log.Error("Error adding book issue : {ErrorMessage}", JsonSerializer.Serialize(result.Message));

            return new ServiceResult<bool>() { Data = false, Message = "BadRequest", Status = StatusType.Failure};
        }
        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            var result = await _issueManager.GetIssues();

            if (result.Status == StatusType.Success)
            {
                Log.Information("Booksissues : {Issues}", JsonSerializer.Serialize(result.Data));
                return Ok(result.Data);
            }

            return NotFound(result.Message);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var result = await _issueManager.GetIssueById(id);

            if (result.Status == StatusType.Success)
            {
                Log.Information("Book issues information : {info}", JsonSerializer.Serialize(result.Data));
                return Ok(result.Data);
            }

            return NotFound(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            //get the user id using JWT token
            //var userId = User.FindFirstValue("UserId");
            var userID = "1";
            Log.Information("User ID of the person who deleted issued the book: {UserId}", userID);
            var result = await _issueManager.DeleteIssue(id, userID);

            if (result.Status == StatusType.Success)
            {
                return Ok(result.Message);
            }
            Log.Error(result.Message);
            return NotFound(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, IssueRequest issuesRequest)
        {
            if (id != issuesRequest.Id)
            {
                return BadRequest("Invalid issue ID");
            }
            //get the user id using JWT token
            //var userId = User.FindFirstValue("UserId");
            var userID = "1";
            Log.Information("User ID of the person who issued the book: {UserId}", userID);
            var result = await _issueManager.UpdateIssue(issuesRequest, userID);

            if (result.Status == StatusType.Success)
            {
                Log.Information("The book issue has been updated : {updated}", JsonSerializer.Serialize(result.Message));
                return Ok(result.Message);
            }

            return NotFound(result.Message);
        }

    }
}
