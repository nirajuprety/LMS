using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
            try
            {
                bool validateEMail = IsEmailValid(staffRequest.Email);

                if (!validateEMail)
                {
                    Log.Information("Email validation failed for: " + staffRequest.Email);
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Email isnot valid",
                        Status = StatusType.Failure
                    };
                }

                if (!IsPasswordValid(staffRequest.Password))
                {
                    Log.Information("Password validation failed for: " + staffRequest.Password);
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Password is not valid",
                        Status = StatusType.Failure
                    };
                }

                // Hash the password before storing it
                string hashedPassword = HashPassword(staffRequest.Password);



                var isEmailUnique = await _service.IsUniqueEmail(staffRequest.Email);
                if (isEmailUnique == true)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Email already exist!",
                        Status = StatusType.Failure
                    };
                }

                // Check if the StaffCode is unique
                var isStaffCodeExist = await _service.IsUniqueStaffCode(staffRequest.StaffCode);
                if (isStaffCodeExist == true)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "The provided StaffCode already exists in the system",
                        Status = StatusType.Failure
                    };
                }

                //Check if stafftype is admin or staff.
                if (staffRequest.StaffType != StaffType.Admin && staffRequest.StaffType != StaffType.Staff)
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Invalid StaffType value. StaffType can only be 0 (admin) or 1 (staff).",
                        Status = StatusType.Failure
                    };
                }

                // Validate that the first character of the name starts with a capital letter
                if (!char.IsUpper(staffRequest.Name.FirstOrDefault()))
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Name should start with a capital letter",
                        Status = StatusType.Failure
                    };
                }


                // Validate that the first character of the username starts with a capital letter
                if (!char.IsUpper(staffRequest.Username.FirstOrDefault()))
                {
                    return new ServiceResult<bool>()
                    {
                        Data = false,
                        Message = "Username should start with a capital letter",
                        Status = StatusType.Failure
                    };
                }


                var vm = _mapper.Map<EStaff>(staffRequest);
                vm.IsDeleted = false;
                vm.Password = hashedPassword;
                vm.IsActive = true;

                vm.CreatedDate = DateTime.Now.ToUniversalTime();
                vm.UpdatedDate = DateTime.Now.ToUniversalTime();


                int staffId = await _service.AddStaff(vm);

                if (staffRequest.StaffType == Domain.Enum.StaffType.Staff)
                {
                    //adding the staff information in Member table
                    EMember member = new EMember()
                    {
                        Email = staffRequest.Email,
                        FullName = staffRequest.Name,
                        MemberType = Domain.Enum.MemberType.Staff,
                        ReferenceId = staffId,
                    };
                    await _memberService.CreateMember(member);
                }
                //adding Login details to the LoginTable
                ELogin login = new ELogin()
                {
                    Email = staffRequest.Email,
                    Password = hashedPassword, //staffRequest.Password,
                    StaffId = staffId
                };
                bool staffLogin = await _service.CreateLogin(login);


                Log.Information("Staff added successfully!: " + JsonSerializer.Serialize(staffRequest));
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
                    Message = "Something went wrong",
                    Status = StatusType.Failure
                };
            }
        }

        public bool IsEmailValid(string email)
        {
            const string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool IsPasswordValid(string password)
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

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
        }


        public async Task<ServiceResult<List<StaffResponse>>> GetAllStaff()
        {
            var staffList = await _service.GetAllStaff();
            var result = (from s in staffList
                          where s.IsDeleted == false         //Exclude those which are deleted
                          select new StaffResponse()
                          {
                              Id = s.Id,
                              Name = s.Name,
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

            var staff = await _service.GetStaffById(id);
            if (staff == null)
            {
                return new ServiceResult<StaffResponse>()
                {
                    Data = null,
                    Message = "Staff not found successfully!",
                    Status = StatusType.Failure,
                };
            }
            var result = new StaffResponse()
            {
                Id = staff.Id,
                Username = staff.Username,
                Password = staff.Password,
                Name = staff.Name,
                Email = staff.Email,
                CreatedDate = staff.CreatedDate,
                UpdatedDate = staff.UpdatedDate,
                IsDeleted = staff.IsDeleted,
                IsActive = staff.IsActive,
                StaffCode = staff.StaffCode,
                StaffType = staff.StaffType
            };
            return new ServiceResult<StaffResponse>()
            {
                Data = result,
                Message = "Staff found successfully!",
                Status = StatusType.Success,
            };
        }


        public async Task<ServiceResult<bool>> UpdateStaff(StaffUpdateRequest staffRequest)
        {
            var staffList = await _service.GetStaffById(staffRequest.Id);
            if (staffList == null)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Staff not found!",
                    Status = StatusType.Failure
                };
            }
            var vm = _mapper.Map<EStaff>(staffRequest);
    

            var result = await _service.UpdateStaff(vm);

            if (staffRequest.StaffType != StaffType.Admin)
            {
                var staffMemberMapper = _mapper.Map<EMember>(staffRequest);
                staffMemberMapper.MemberCode = staffRequest.StaffCode;
                staffMemberMapper.FullName = staffRequest.Name;
                staffMemberMapper.ReferenceId = staffRequest.Id;
                await _memberService.UpdateMember(staffMemberMapper);
            }
            var staffLoginMapper = _mapper.Map<ELogin>(staffRequest);
            staffLoginMapper.StaffId = staffRequest.Id;
            await _service.UpdateUser(staffLoginMapper);

            return new ServiceResult<bool>()
            {
                Data = result,
                Message = result == true ? "Staff Updated Successfully!" : "Unable to Update Staff",
                Status = result == true ? StatusType.Success : StatusType.Failure
            };
        } 
        public async Task<ServiceResult<bool>> DeleteStaff(int id)
        {
            var staffList = await _service.DeleteStaff(id);
            await _memberService.DeleteMember(id);
            await _service.DeleteUser(id);
            if (staffList == false)
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
