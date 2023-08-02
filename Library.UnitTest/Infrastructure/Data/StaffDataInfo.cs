using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UnitTest.Infrastructure.Data
{
    public class StaffDataInfo
    {
        private static string hashedPassword;

        public static void Init()
        {
            StaffRequest = new StaffRequest()
            {
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuyel6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffCode = 1,
                StaffType = Domain.Enum.StaffType.Admin
            };

            eStaffRequest = new EStaff()
            {
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuye66@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffCode = 11,
                StaffType = Domain.Enum.StaffType.Admin
            };
            //eStaffRequestWithId = new EStaff()
            //{
            //    Id=1,
            //    Username = "SamikshaPhuyel",
            //    Email = "Samikshaphuye66@gmail.com",
            //    Name = "Samiksha",
            //    Password = "Samiksha123!",
            //    StaffCode = 2,
            //    StaffType = Domain.Enum.StaffType.Admin
            //};

            eStaffUpdateRequest = new EStaff()
            {
                Id = 1,
                Username = "SamikshaPhuyel",
                Email = "Samikshaphuyel6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffType = Domain.Enum.StaffType.Admin,
                UpdatedDate = DateTime.Today.ToUniversalTime(),
                StaffCode = 2,
                IsDeleted = false,
                IsActive = true
            };

            LoginInfo = new ELogin()
            {
                Email = "SamikshaPhuyel@gmail.com",
                Password = hashedPassword,
                StaffId = 1
            };

            StaffResponseList = new List<StaffResponse>()
            {
                new StaffResponse()
                {
                    Id=1,
                    Username="samikshaphuyel",
                    Password="Samiksha123!",
                    Name="Samiksha",
                    Email = "Samikshaphuyel6@gmail.com",
                    CreatedDate=DateTime.Today.ToUniversalTime(),
                    UpdatedDate = DateTime.Today.ToUniversalTime(),
                    IsDeleted =false,
                    IsActive=true,
                    StaffCode=1,
                    StaffType=StaffType.Staff
                }
            };

            eStaffList = new List<EStaff>()
            {
                 new EStaff()
                {
                    Id=1,
                    Username="samikshaphuyel",
                    Name="Samiksha",
                    Password="Samiksha123!",
                    Email = "Samikshaphuyel6@gmail.com",
                    CreatedDate=DateTime.Today.ToUniversalTime(),
                    UpdatedDate = DateTime.Today.ToUniversalTime(),
                    IsDeleted =false,
                    IsActive=true,
                    StaffCode=1,
                    StaffType=StaffType.Staff
                }
            };

            StaffResponse = new StaffResponse()
            {

                Id = 1,
                Username = "samikshaphuyel",
                Email = "Samikshaphuyel6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffType = Domain.Enum.StaffType.Staff,
                UpdatedDate = DateTime.Today.ToUniversalTime(),
                CreatedDate = DateTime.Today.ToUniversalTime(),
                StaffCode = 1,
                IsDeleted = false,
                IsActive = true

            };


            eStaffResponse = new EStaff()
            {
                Id = 1,
                Username = "samikshaphuyel",
                Email = "Samikshaphuyel6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffType = Domain.Enum.StaffType.Staff,
                UpdatedDate = DateTime.Today.ToUniversalTime(),
                CreatedDate = DateTime.Today.ToUniversalTime(),
                StaffCode = 1,
                IsDeleted = false,
                IsActive = true
            };

            eStaffResponseWithId = new EStaff()
            {
                Id = 2,
                Username = "samikshaphuyel",
                Email = "Samikshaphuyel6@gmail.com",
                Name = "Samiksha",
                Password = "Samiksha123!",
                StaffType = Domain.Enum.StaffType.Staff,
                UpdatedDate = DateTime.Today.ToUniversalTime(),
                CreatedDate = DateTime.Today.ToUniversalTime(),
                StaffCode = 1,
                IsDeleted = false,
                IsActive = true
            };

            StaffUpdateRequest = new StaffUpdateRequest()
            {
                Id = 1,
                Username = "samikshaphuyel",
                Password = "Samiksha123!!!",
                Name = "Samiksha",
                Email = "Samikshaphuyel6@gmail.com",
                StaffCode = 3,
                StaffType = StaffType.Staff

            };

            eLogin = new ELogin()
            {
                Email = "Samikshaphuyel6@gmail.com",
                Password = "Samiksha123!!",
                StaffId = 1,

            };

            eMember = new EMember()
            {
                Id = 1,
                Email = "Samikshaphuyel6@gmail.com",
                FullName = "Samiksha",
                MemberCode = 1,
                MemberType = MemberType.Staff,
                ReferenceId = 1
            };
        }

        public static StaffRequest StaffRequest { get; set; }
        public static EStaff eStaffRequest { get; set; } = new EStaff();
        public static EStaff eStaffUpdateRequest { get; set; } = new EStaff();
        public static ELogin LoginInfo { get; set; }

        public static List<StaffResponse> StaffResponseList { get; set; }
        public static List<EStaff> eStaffList { get; set; }
        public static EStaff eStaffResponse { get; set; }
        public static EStaff eStaffResponseWithId { get; set; }

        public static StaffResponse StaffResponse { get; set; }
        public static StaffUpdateRequest StaffUpdateRequest { get; set; }
        public static ELogin eLogin { get; set; } = new ELogin();

        public static EMember eMember { get; set; } = new EMember();




    }
}


