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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{

    public class StaffManager : IStaffManager
    {
        private readonly IStaffService _service = null;
        private readonly ILoginService _serviceLogin = null;
        private readonly IMapper _mapper = null;
        private readonly IMemberService _memberService = null;

        public StaffManager(IStaffService service, IMapper mapper, ILoginService serviceLogin, IMemberService memberService)
        {
            _service = service;
            _mapper = mapper;
            _serviceLogin = serviceLogin;
            _memberService = memberService;
        }

        public async Task<ServiceResult<bool>> AddStaff(StaffRequest staffRequest)
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
            if(staffRequest.StaffType == Domain.Enum.StaffType.Staff)
            {

                //adding the staff information in Member table
                EMember member = new EMember()
                {
                    Email = staffRequest.Email,
                    FullName = staffRequest.Name,
                    MemberType = Domain.Enum.MemberType.Staff,
                    MemberCode = staffRequest.StaffCode,
                    ReferenceId = staffId,
                };
                await _memberService.CreateMember(member);
            }
            

           

                //adding Login details to the LoginTable
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
            catch (Exception ex)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = ex.Message,
                    Status = StatusType.Failure
                };
            }
        }
        private bool IsEmailValid(string email)
        {
            const string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsPasswordValid(string password)
        {
            const int MinimumLength = 5;
            const string SpecialCharacters = @"!@#$%^&*()-_=+[{]}\|;:'"",<.>/?";
            const string AlphanumericPattern = @"^(?=.*[a-zA-Z])(?=.*[0-9])";

            if (password.Length < MinimumLength)
            {
                return false;
            }

            if (!password.Any(c => SpecialCharacters.Contains(c)))
            {
                return false;
            }

            if (!Regex.IsMatch(password, AlphanumericPattern))
            {
                return false;
            }

            return true;
        } 



        public async Task<ServiceResult<List<StaffResponse>>> GetAllStaff()
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
                              IsDeleted = s.IsDeleted,
                              IsActive = s.IsActive,
                              StaffCode = s.StaffCode,
                              StaffType = s.StaffType

                          }).ToList();
            return new ServiceResult<List<StaffResponse>>()
            {
                Data = result,
                Message = "All Staff found successfully!",
                Status = StatusType.Success,
            };
        }

        public async Task<ServiceResult<StaffResponse>> GetStaffById(int id)
        {

            var staffList = await _service.GetStaffById(id);
            if (staffList == null)
            {
                return new ServiceResult<StaffResponse>()
                {
                    Data = new StaffResponse()
                    { 
                        Id = id
                    },
                    Status = StatusType.Failure,
                    Message = "Staff not found"
                };
            }
            var result = new StaffResponse()
            {
                Id = staffList.Id,
                Username = staffList.Username,
                Password = staffList.Password,
                Name= staffList.Name,
                Email = staffList.Email,
                CreatedDate = staffList.CreatedDate,
                UpdatedDate = staffList.UpdatedDate,
                IsDeleted = staffList.IsDeleted,
                IsActive = staffList.IsActive,
                StaffCode = staffList.StaffCode,
                StaffType = staffList.StaffType
            };
            return new ServiceResult<StaffResponse>()
            {
                Data = result,
                Message = "Staff found successfully!",
                Status = StatusType.Success,
            };
        }


        public async Task<ServiceResult<bool>> UpdateStaff(StaffRequest staffRequest)
        {
            var staffList = await _service.GetStaffById(staffRequest.Id);
            var vm = _mapper.Map<EStaff>(staffRequest);
            var result = await _service.UpdateStaff(vm);
            return new ServiceResult<bool>()
            {
                Data = result,
                Message = result == true ? "Staff Updated Successfully!" : "Unable to Update Staff",
                Status = result==true? StatusType.Success: StatusType.Failure
            };
        }
        public async Task<ServiceResult<bool>> DeleteStaff(int id)
        {
            var staffList = await _service.DeleteStaff(id);
            if (staffList == null)
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Unable to delete staff",
                    Status = StatusType.Failure
                };
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = "Staff deleted successfully!",
                Status = StatusType.Success
            };

        }
    }
}
