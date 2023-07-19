using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Interface
{
    public interface IStudentManager
    {
        Task<ServiceResult<bool>> CreateStudent(StudentRequest studentRequest);
        Task<ServiceResult<List<StudentResponse>>> GetStudents();
        Task<ServiceResult<StudentResponse>> GetStudentByID(int id);
        Task<ServiceResult<bool>> UpdateStudent(StudentRequest studentRequest);
        Task<ServiceResult<bool>> DeleteStudent(int id);
    }
}
