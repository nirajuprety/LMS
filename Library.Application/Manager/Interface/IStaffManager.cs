using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Interface
{
    public interface IStaffManager
    {
        Task<ServiceResult<bool>> AddStaff(StaffRequest staffRequest);
        Task<ServiceResult<List<StaffResponse>>> GetAllStaff();
        Task<ServiceResult<StaffResponse>> GetStaffById(int id);
        Task<ServiceResult<bool>>UpdateStaff(StaffUpdateRequest staffRequest);
        Task<ServiceResult<bool>>DeleteStaff(int id);

                                                                   
    }
}
