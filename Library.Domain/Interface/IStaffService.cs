using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IStaffService
    {
        Task<int>CreateStaff(EStaff eStaff);
        Task<bool> CreateLogin(ELogin log);
        Task<List<EStaff>> GetAllStaff();
        Task<EStaff> GetStaffById(int id);
        Task<bool>UpdateStaff(EStaff eStaff);


    }
}
