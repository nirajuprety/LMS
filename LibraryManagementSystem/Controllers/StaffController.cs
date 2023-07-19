using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using static Library.Infrastructure.Service.Common;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffManager _manager = null;
        public StaffController(IStaffManager manager)
        {
            _manager = manager;
        }
        [HttpPost("AddStaff")]
        public async Task<ServiceResult<bool>> AddStaff(StaffRequest staffRequest)
        {
            var result = await _manager.AddStaff(staffRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet("GetStaff")]

        public async Task<ServiceResult<List<StaffResponse>>>GetAllStaff()
        {
            var result = await _manager.GetAllStaff();
            return new ServiceResult<List<StaffResponse>>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };

        }
        [HttpGet("GetStaffById")]
        public async Task<ServiceResult<StaffResponse>>GetStaffBbyId(int id)
        {
            var result= await _manager.GetStaffById(id);
            return new ServiceResult<StaffResponse>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        //[HttpPut("UpdateStaff")]

        public async Task<ServiceResult<bool>>UpdateStaff(StaffRequest staffRequest)
        {
            var result = await _manager.UpdateStaff(staffRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }


        [HttpDelete("DeleteStaff")]

        public async Task<ServiceResult<bool>>DeleteStaff(int id)
        {
           var result= await _manager.DeleteStaff(id);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }
    }
}
