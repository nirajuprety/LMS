using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Infrastructure.Service.Common;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Staff")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager _manager;
        public StudentController(IStudentManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<ServiceResult<bool>> CreateStudent(StudentRequest studentRequest)
        {

            var result = await _manager.CreateStudent(studentRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpPut]
        public async Task<ServiceResult<bool>> DeleteStudent(int id)
        {
            var result = await _manager.DeleteStudent(id);
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
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }
    }
}
