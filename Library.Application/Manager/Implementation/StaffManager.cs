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

    public class StaffManager:IStaffManager
    {
        private readonly IStaffService _service = null;
        //private readonly IloginService _serviceLogin = null;
        private readonly IMapper _mapper = null;

        public StaffManager(IStaffService service, IMapper mapper, IloginService serviceLogin)
        {
            _service = service;
            _mapper = mapper;
            _serviceLogin = serviceLogin;
        }

        public async Task<ServiceResult<bool>> CreateStaff(StaffRequest staffRequest)
        {
            var vm = _mapper.Map<EStaff>(staffRequest);

            var result = await _service.CreateStaff(vm);
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = "Staff Signedup succesfully!",
                Status = StatusType.Success
            };

        }
    }
}
