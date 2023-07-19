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
        [HttpPost("CreateStaff")]
        public async Task<ServiceResult<bool>> CreateStaff(StaffRequest staffRequest)
        {
            var result = await _manager.CreateStaff(staffRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet("GetStaff")]

        public async Task<List<StaffResponse>> GetAllStaff()
        {
            var result = await _manager.GetAllStaff();
            return result;

        }
        [HttpGet("GetStaffById")]
        public async Task<StaffResponse>GetStaffBbyId(int id)
        {
            var result= await _manager.GetStaffById(id);
            return result;
        }

        [HttpPut("UpdateStaff")]

        public async Task<bool>UpdateStaff(StaffRequest staffRequest)
        {
            var result = await _manager.UpdateStaff(staffRequest);
                return result;
        }



    }
}
