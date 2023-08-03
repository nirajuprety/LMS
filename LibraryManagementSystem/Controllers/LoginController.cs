using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Library.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Serilog;
using System.Text.Json;
using static Library.Infrastructure.Service.Common;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;
        private readonly IConfiguration _configuration = null;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILoginManager manager, IConfiguration configuration,ILogger<LoginController> logger)
        {
            _loginManager = manager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("LoginUser")]
        public async Task<ServiceResult<bool>> LoginUser(LoginRequest request)
        {
            var result = await _loginManager.LoginUser(request);
            if(result.Status == StatusType.Success)
            {
                Log.Information("Login done successfully: {user}", JsonSerializer.Serialize(request));
                return new ServiceResult<bool>()
                {
                    Data = result.Data,
                    Message = result.Message,
                    Status = StatusType.Success
                };
            }
                Log.Information("Login Failed: {user}", JsonSerializer.Serialize(result.Message));
            return new ServiceResult<bool>()
            {
                Data = result.Data,
                Message = "Login Failed",
                Status = StatusType.Failure
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
