using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IStudent
    {
        Task<bool> CreateStudent(EStudent eStudent);
        Task<List<EStudent>> GetStudents();
        Task<EStudent> GetStudentByID(int id);
        Task<bool> UpdateStudent(EStudent eStudent);
        Task<bool> DeleteStudent(int id);
    }
}
