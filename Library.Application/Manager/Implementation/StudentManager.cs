using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

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
        public async Task<ServiceResult<bool>> CreateStudent(StudentRequest studentRequest)
        {
            try
            {
                var model = _mapper.Map<EStudent>(studentRequest);
                var result = await _service.CreateStudent(model);
                return new ServiceResult<bool>()
                {
                    Data = result,
                    Status = StatusType.Success,
                    Message = "User Created Successfull"
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ServiceResult<bool>> DeleteStudent(int id)
        {
            try
            {
                var result = await _service.DeleteStudent(id);
                if (result == false)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = result,
                        Status = StatusType.Failure,
                        Message = "User not found"
                    };
                }
                return new ServiceResult<bool>()
                {
                    Data = result,
                    Status = StatusType.Success,
                    Message = "User Delete Successfull"
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ServiceResult<StudentResponse>> GetStudentByID(int id)
        {
            try
            {
                var user = await _service.GetStudentByID(id);
                //var model = _mapper.Map<EStudent>(StudentResponse);
                var result = new StudentResponse()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Faculty = user.Faculty,
                    FullName = user.FullName,
                    RollNo = user.RollNo,
                    StudentCode = user.StudentCode,
                };
                if (result == null)
                {
                    return new ServiceResult<StudentResponse>()
                    {
                        Data = result,
                        Status = StatusType.Failure,
                        Message = "User not found"
                    };
                }
                return new ServiceResult<StudentResponse>()
                {
                    Data = result,
                    Status = StatusType.Success,
                    Message = "User Found Successfull"
                };
            }
            catch (Exception ex) { throw; }
        }

        public async Task<ServiceResult<List<StudentResponse>>> GetStudents()
        {
            try {
                var user = await _service.GetStudents();
                var result = (from u in user
                              select new StudentResponse()
                              {
                                  Id = u.Id,
                                  Email = u.Email,
                                  Faculty = u.Faculty,
                                  FullName = u.FullName,
                                  RollNo = u.RollNo,
                                  StudentCode = u.StudentCode
                              }
                             ).ToList();
                return new ServiceResult<List<StudentResponse>>()
                {
                    Data = result,
                    Status = StatusType.Success,
                    Message = "Users List"
                };
            } catch (Exception ex) { throw; } 
        }

        public async Task<ServiceResult<bool>> UpdateStudent(StudentRequest studentRequest)
        {
            var model = _mapper.Map<EStudent>(studentRequest);
            model.Id = studentRequest.Id;
            var user = await _service.UpdateStudent(model);
            return new ServiceResult<bool>()
            {
                Data = user,
                Status = user == true ? StatusType.Success : StatusType.Failure,
                Message = user == true ? "Student updated successfull" : "Student not found"
            };
        }
    }
}
