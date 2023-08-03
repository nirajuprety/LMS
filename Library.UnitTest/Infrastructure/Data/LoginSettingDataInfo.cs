using Library.Application.DTO.Request;
using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.UnitTest.Infrastructure.Data
{
    public static class LoginSettingDataInfo
    {
        public static void Init()
        {

            LoginSetting = new List<ELogin>()
            {
                new ELogin()
                {
                    Id = 1,
                    Email = "nirajuprety@gmail.com",
                    Password = "niraj1234!",
                    StaffId = 1,
                },
            };

            LoginRequestData = new LoginRequest()
            {
                Email = "user@gmail.com",
                Password = "user1234!"
            };

            SuccessLoginData = new ServiceResult<bool>()
            {
                Data = true,
                Message = "Success Login",
                Status = StatusType.Success,
            };
            LoginData = new ELogin()
            {
                Id = 1,
                Email = "user@gmail.com",
                Password = "gAqS6J+OlZQLr1CDq0yKqO3feLK/ujnCpYL0NL/elUw=",
                StaffId = 1,
                
            };
             LoginStaffData = new ELogin()
            {
                Id = 2,
                Email = "staff@gmail.com",
                Password = "staff",
                StaffId = 2,
                
            };

            //LoginStaffType = new EStaff()
            //{
            //    StaffType="Admin",
            //    StaffType="Staff"
            //};
            LoginDataEmail = new ELogin()
            {
                Email="NirajNiharika@gmail.com",

            };



        }
        public static List<ELogin> LoginSetting { get; private set; } = new List<ELogin>();
        public static LoginRequest LoginRequestData{ get; private set; } = new LoginRequest();
        public static ServiceResult<bool> SuccessLoginData{ get; private set; } = new ServiceResult<bool>();
        public static ELogin LoginData{ get; private set; } = new ELogin();
        public static ELogin LoginStaffData{ get; private set; } = new ELogin();
        public static ELogin LoginDataEmail { get; private set; } = new ELogin();

    }
}
