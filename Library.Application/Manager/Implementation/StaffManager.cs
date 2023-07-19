using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{

    public class StaffManager : IStaffManager
    {
        private readonly IStaffService _service = null;
        private readonly ILoginService _serviceLogin = null;
        private readonly IMapper _mapper = null;

        public StaffManager(IStaffService service, IMapper mapper, ILoginService serviceLogin)
        {
            _service = service;
            _mapper = mapper;
            _serviceLogin = serviceLogin;
        }

        public async Task<ServiceResult<bool>> CreateStaff(StaffRequest staffRequest)
        {
            var vm = _mapper.Map<EStaff>(staffRequest);

            //EStaff vm = new EStaff()
            //{
            //    IsActive = true,
            //    CreatedDate = DateTime.Now,
            //    Email = staffRequest.Email,
            //    IsDeleted = false,
            //    Name = staffRequest.Name,
            //    Password = staffRequest.Password,
            //    StaffCode = staffRequest.StaffCode,
            //    StaffType = Domain.Enum.StaffType.Staff,
            //    UpdatedDate = DateTime.Now,
            //    Username = staffRequest.Username,
            //};

            int staffId = await _service.CreateStaff(vm);
            var login = new ELogin()
            {
                Email = vm.Email,
                Password = vm.Password,
                StaffId = staffId
            };
            bool staffLogin = await _service.CreateLogin(login);
            return new ServiceResult<bool>()

            {
                Data = staffLogin,
                Message = staffLogin == true ? "Added successfully" : "unable to add the staff",
                Status = staffLogin == true ? StatusType.Success : StatusType.Failure
            };

        }

        public async Task<List<StaffResponse>> GetAllStaff()
        {
            var staffList = await _service.GetAllStaff();
            var result = (from s in staffList
                          select new StaffResponse()
                          {
                              Id = s.Id,
                              Username = s.Username,
                              Password = s.Password,
                              Email = s.Email,
                              CreatedDate = s.CreatedDate,
                              UpdatedDate = s.UpdatedDate,
                              IsDeleted= s.IsDeleted,
                              IsActive= s.IsActive,
                              StaffCode=s.StaffCode,
                              StaffType= s.StaffType

                          }).ToList();
            return result;

        }

        public async Task<StaffResponse> GetStaffById(int id)
        {
            var staffList=await _service.GetStaffById(id);
            var result = new StaffResponse()
            {
                Id = staffList.Id,
                Username = staffList.Username,
                Password = staffList.Password,
                Email = staffList.Email,
                CreatedDate = staffList.CreatedDate,
                UpdatedDate = staffList.UpdatedDate,
                IsDeleted = staffList.IsDeleted,
                IsActive = staffList.IsActive,
                StaffCode = staffList.StaffCode,
                StaffType = staffList.StaffType
            };
            return result;
        }
    }
}
    

