using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class StudentService : IStudentService
    {
        private readonly IServiceFactory _factory;
        //private readonly IConfiguration _configuration;

        public StudentService(IServiceFactory factory)
        {
            _factory = factory;
            //_configuration = configuration;
        }

        public async Task<bool> CreateStudent(EStudent eStudent)
        {
            try
            {
                var service = _factory.GetInstance<EStudent>();
                await service.AddAsync(eStudent);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var service = _factory.GetInstance<EStudent>();
                var user = await service.FindAsync(id);

                if (user == null)
                {
                    return false;
                }
                user.IsDeleted = true;
                user.IsActive = false;
                user.CreatedDate = DateTime.UtcNow; // this is done to fix issue with "timestamp with time zone"

                await service.UpdateAsync(user);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EStudent> GetStudentByID(int id)
        {
            try
            {
                var service = _factory.GetInstance<EStudent>();
                var user = await service.FindAsync(id);
                if (user == null)
                {
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<EStudent>> GetStudents()
        {
            try
            {
                var service = _factory.GetInstance<EStudent>();
                var result = await service.ListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateStudent(EStudent eStudent)
        {
            try
            {
                var service = _factory.GetInstance<EStudent>();
                var user = await service.FindAsync(eStudent.Id);
                if (user == null)
                {
                    return false;
                }
                if (user.IsDeleted == true)
                {
                    return false;
                }
                user.FullName = eStudent.FullName;
                user.Email = eStudent.Email;
                user.Faculty = eStudent.Faculty;
                user.RollNo = eStudent.RollNo;
                user.CreatedDate = eStudent.CreatedDate;

                await service.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> IsUniqueEmail(string email)
        {
            var student = await _factory.GetInstance<EStudent>().ListAsync();
            var result = student.Where(student => student.Email == email).Any();
            return result;
        }
    }
}
