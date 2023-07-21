using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
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

        public async Task<ServiceResult<MemberResponse>> GetMemberById(int id)
        {
            var serviceResult = new ServiceResult<MemberResponse>();

            try
            {
                var member = await _service.GetMemberById(id);
                if (member == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Member not found";
                    serviceResult.Data = null;

                    return serviceResult;
                }

                var memberResponse = _mapper.Map<MemberResponse>(member);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Member retrieved successfully";
                serviceResult.Data = memberResponse;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the Member";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<List<MemberResponse>>> GetMembers()
        {
            var serviceResult = new ServiceResult<List<MemberResponse>>();

            try
            {
                var members = await _service.GetMembers();
                var membersResponses = _mapper.Map<List<MemberResponse>>(members);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Members retrieved successfully";
                serviceResult.Data = membersResponses;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the members";
                serviceResult.Data = null;

                return serviceResult;
            }
        }
    }
}

        
