using Library.Domain.Entities;
using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface ILoginService
    {
        Task<ELogin> GetUserById(int id);
        Task<bool> AddLogin(ELogin login, int staffId);
        Task<bool> LoginUser(ELogin eLogin);
        //Task<bool> UpdateUser(ELogin eLogin);
        Task<bool> ValidateEmail(string email);
        Task<StaffType> GetUserRole(string email);
        Task<int> GetUserId(string email);
    }
}
