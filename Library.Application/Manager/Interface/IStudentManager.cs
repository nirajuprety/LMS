using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Manager.Interface
{
    public interface IStudentManager
    {
        Task<bool> CreateStudent(StudentRequest studentRequest);
        Task<List<StudentResponse>> GetStudents();
        Task<StudentResponse> GetStudentByID(int id);
        Task<bool> UpdateStudent(StudentRequest studentRequest);
        Task<bool> DeleteStudent(int id);
    }
}
