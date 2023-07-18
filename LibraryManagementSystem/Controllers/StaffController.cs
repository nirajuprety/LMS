using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ServiceResult<bool>>CreateStaff(StaffRequest staffRequest)
        {
            var result = await _manager.CreateStaff(staffRequest);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }


    }
}
