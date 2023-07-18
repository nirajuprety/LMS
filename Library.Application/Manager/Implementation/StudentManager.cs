using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Manager.Implementation
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentService _service;
        private readonly IMapper _mapper;

        public StudentManager(IStudentService studentService, IMapper mapper)
        { 
            _service = studentService;
            _mapper = mapper;
        }
        public async Task<bool> CreateStudent(StudentRequest studentRequest)
        {
            try {
                var parse = new EStudent()
                {
                    FullName = studentRequest.FullName,
                    Email = studentRequest.Email,
                    Faculty = studentRequest.Faculty,
                    RollNo = studentRequest.RollNo,
                    StudentCode = studentRequest.StudentCode,
                };
                var result = await _service.CreateStudent(parse);
                return result;
            } catch (Exception ex) {
                return false;
            }
        }

        public Task<bool> DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentResponse> GetStudentByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentResponse>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStudent(StudentRequest studentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
