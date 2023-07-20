using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class LoginService : ILoginService
    {
        private readonly IServiceFactory _factory;

        public LoginService(IServiceFactory factory)
        {
            _factory = factory;
        }
        public async Task<bool> AddLogin(ELogin login, int staffId)
        {
            var users = _factory.GetInstance<ELogin>();
            var parse = new ELogin()
            {
                StaffId = staffId,
                Email = login.Email,
                Password = login.Password,
            };
            await users.AddAsync(parse);
            return true;
        }

        public async Task<ELogin> GetUserById(int id)
        {
            try
            {
                var service = _factory.GetInstance<ELogin>();
                var user = await service.FindAsync(id);
                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<StaffType> GetUserRole(string email)
        {
            var user = await _factory.GetInstance<EStaff>().ListAsync();
            var staffRole= user.Where(x => x.Email.Trim() == email.Trim()).Select(x => x.StaffType).FirstOrDefault();
            return staffRole;

        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
        }
        public async Task<bool> LoginUser(ELogin eLogin)
        {
            var user = _factory.GetInstance<ELogin>().ListAsync();
            var userEmail = user.Result.Any(x => x.Email == eLogin.Email && x.Password == HashPassword(eLogin.Password));

            return userEmail;
        }
        public async Task<bool> ValidateEmail(string email)
        {
            var userEmail = _factory.GetInstance<ELogin>().ListAsync();
            bool IsValidEmail = userEmail.Result.Any(x => x.Email.ToLower() == email.ToLower());
            return IsValidEmail;
        }
    }
}
