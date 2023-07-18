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
    public class MemberManager : IMemberManager
    {
        private readonly IMemberService _service = null;
        //private readonly ILoginService _serviceLogin = null;
        private readonly IMapper _mapper = null;

        public MemberManager(IMemberService service, IMapper mapper, ILoginService serviceLogin)
        {
            _service = service;
            _mapper = mapper;
            //_serviceLogin = serviceLogin;
        }

        public async Task<ServiceResult<bool>> CreateMember(MemberRequest request)
        {
            //mapper
            var vm = _mapper.Map<EMember>(request);
            
            int memberId = await _service.CreateMember(vm);
            return new ServiceResult<bool>()

            {
                Data = false,
                Message = "Added successfully",
                Status = StatusType.Success,
            };
        }

    }
}
