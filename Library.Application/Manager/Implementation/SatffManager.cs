using AutoMapper;
using Library.Application.DTO.Request;
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
            ELogin login = new ELogin()
            {
                Email = staffRequest.Email,
                Password = staffRequest.Password,
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
    }
}
