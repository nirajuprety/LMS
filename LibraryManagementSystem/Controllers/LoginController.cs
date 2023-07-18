using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _manager = null;
        private readonly IConfiguration _configuration = null;
        public LoginController(ILoginManager manager, IConfiguration configuration)
        {
            _manager = manager;
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task<ServiceResult<bool>> LoginUser(LoginRequest request)
        {
            var result = await _manager.LoginUser(request);
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        //[Authorize]
        [HttpGet("UserDashboard")]
        public async Task<ServiceResult<bool>> UserDashboard()
        {
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = "",
                Status = StatusType.Success
            };
        }

    }
}
