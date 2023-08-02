using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{
    public class LoginManager : ILoginManager
    {
        public IConfiguration _configuration;
        private readonly ILoginService _service;
        private readonly IMapper _mapper = null;

        public LoginManager(ILoginService service, IMapper mapper, IConfiguration config)
        {
            _service = service;
            _mapper = mapper;
            _configuration = config;
        }
        public async Task<ServiceResult<bool>> LoginUser(LoginRequest loginRequest)
        {
            bool IsValidemail = await _service.ValidateEmail(loginRequest.Email);

            if (!IsValidemail)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Email",
                    Status = StatusType.Failure
                };
            }
            var parse = new ELogin()

            {
                Email = loginRequest.Email,
                Password = loginRequest.Password,
            };
            bool IsLogin = await _service.LoginUser(parse);
            if (!IsLogin)
            {
                return new ServiceResult<bool>()
                {
                    Data = false,
                    Message = "Password",
                    Status = StatusType.Failure
                };

            }

            string userToken = await GetToken(loginRequest);
            return new ServiceResult<bool>()
            {
                Data = true,
                Message = userToken,
                Status = StatusType.Success
            };


        }

        public async Task<string> GetToken(LoginRequest request)
        {
            if (request != null && request.Email != null && request.Password != null)
            {
                bool user = await _service.LoginUser(new ELogin()
                {
                    Email = request.Email,
                    Password = request.Password
                });
                string userRole = _service.GetUserRole(request.Email).Result.ToString();
                if (user != null)
                {
                    // create claims details based on the user information
                    var claims = new[] { 
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", request.Email.ToString()),
                        new Claim(ClaimTypes.Role, userRole),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var userToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return userToken;
                }

            }
            return "";
        }
    }
}
