using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager _manager;
        public StudentController(IStudentManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<bool> CreateStudent(StudentRequest studentRequest)
        { 
            var result = await _manager.CreateStudent(studentRequest);
            return result;
        }
    }
}
