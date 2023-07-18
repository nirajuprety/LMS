using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> AddLogin(ELogin login)
        {
            var users = _factory.GetInstance<ELogin>();
            var parse = new ELogin()
            {
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
        public async Task<bool> LoginUser(ELogin eLogin)
        {
            var user = _factory.GetInstance<ELogin>().ListAsync();
            var userEmail = user.Result.Any(x => x.Email == eLogin.Email && x.Password == eLogin.Password);

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
