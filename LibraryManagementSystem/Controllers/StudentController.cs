using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using static Library.Infrastructure.Service.Common;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Staff")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager _manager;
        private readonly ILogger<StudentController> _logger ;

        public StudentController(IStudentManager manager , ILogger<StudentController> logger)
        {
            _manager = manager;
            _logger = logger;   
        }

        [HttpPost]
        public async Task<ServiceResult<bool>> CreateStudent(StudentRequest studentRequest)
        {

            var result = await _manager.CreateStudent(studentRequest);
            if (result.Status == StatusType.Failure)
            {
                return new ServiceResult<bool>()
                {
                    Data = result.Data,
                    Status = result.Status,
                    Message = result.Message
                };
            }
            Log.Information("Student Created Successfull: {Student}", JsonSerializer.Serialize(studentRequest));
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpDelete]
        public async Task<ServiceResult<bool>> DeleteStudent(int id)
        {
            var result = await _manager.DeleteStudent(id);
            if (result.Status == StatusType.Failure)
            {
                return new ServiceResult<bool>()
                {
                    Data = result.Data,
                    Status = result.Status,
                    Message = result.Message
                };
            }
            Log.Information("Student Deleted Successfull: {Student}", JsonSerializer.Serialize(result.Data));
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpGet]
        public async Task<ServiceResult<StudentResponse>> GetStudentById(int id)
        {
            var result = await _manager.GetStudentByID(id);
            if (result.Status == StatusType.Failure)
            {
                return new ServiceResult<StudentResponse>()
                {
                    Data = result.Data,
                    Status = result.Status,
                    Message = result.Message
                };
            }
            Log.Information("Student get by ID: {Student}", JsonSerializer.Serialize(result.Data));
            return new ServiceResult<StudentResponse>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }
        [HttpGet]
        public async Task<ServiceResult<List<StudentResponse>>> GetStudents()
        {
            var result = await _manager.GetStudents();
            if (result.Status == StatusType.Failure)
            {
                return new ServiceResult<List<StudentResponse>>()
                {
                    Data = result.Data,
                    Status = result.Status,
                    Message = result.Message
                };
            }
            Log.Information("Students Lists: {Student}", JsonSerializer.Serialize(result.Data));
            return new ServiceResult<List<StudentResponse>>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }
        [HttpPut]
        public async Task<ServiceResult<bool>> UpdateStudent(StudentRequest studentRequest)
        {
            var result = await _manager.UpdateStudent(studentRequest);
            if (result.Status == StatusType.Failure)
            {
                return new ServiceResult<bool>()
                {
                    Data = result.Data,
                    Status = result.Status,
                    Message = result.Message
                };
            }
            Log.Information("Student Updated Successfull: {Student}", JsonSerializer.Serialize(result.Data));
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }
    }
}
