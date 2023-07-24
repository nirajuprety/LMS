using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Library.Infrastructure.Service.Common;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LibraryManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

   // [Authorize(Roles = "Admin")]

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

            if (result.Status == StatusType.Success)
            {
                Log.Information("Staff added successfully:" + JsonConvert.SerializeObject(staffRequest));
            }
            else
            {
                Log.Error("Failed to add staff. Error: " + result.Message);
            }
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet("GetStaff")]

        public async Task<ServiceResult<List<StaffResponse>>> GetAllStaff()
        {
            var result = await _manager.GetAllStaff();
            if (result.Status == StatusType.Success)
            {
                Log.Information("All staff retrieved successfully:" + JsonSerializer.Serialize(result.Data));
            }
            else
            {
                Log.Error("Unable to retrieve all staff. Error: " + result.Message);
            }
            return new ServiceResult<List<StaffResponse>>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };

        }
        [HttpGet("GetStaffById")]
        public async Task<ServiceResult<StaffResponse>> GetStaffBbyId(int id)
        {
            var result = await _manager.GetStaffById(id);

            if (result.Status == StatusType.Success)
            {
                Log.Information("Staff retrieved successfully:" + JsonConvert.SerializeObject(result.Data));
            }
            else
            {
                Log.Error("Unable to retrieve the staff. Error: " + result.Message);
            }
            return new ServiceResult<StaffResponse>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut("UpdateStaff")]
        public async Task<ServiceResult<bool>> UpdateStaff(StaffUpdateRequest staffRequest)
        {
            var result = await _manager.UpdateStaff(staffRequest);

            if (result.Status == StatusType.Success)
            {
                Log.Information("Staff updated successfully:" + JsonConvert.SerializeObject(staffRequest));
            }
            else
            {
                Log.Error("Unable to update staff. Error: " + result.Message);
            }
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }


        [HttpDelete("DeleteStaff")]

        public async Task<ServiceResult<bool>> DeleteStaff(int id)
        {
            var result = await _manager.DeleteStaff(id);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }
    }
}
