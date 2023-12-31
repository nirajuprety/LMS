﻿using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IStaffService
    {
        Task<int>AddStaff(EStaff eStaff);
        Task<bool> CreateLogin(ELogin log);
        Task<bool> UpdateUser(ELogin login);
        Task<List<EStaff>> GetAllStaff();
        Task<EStaff> GetStaffById(int id);
        Task<bool>UpdateStaff(EStaff eStaff);

        Task<bool> DeleteStaff(int id);  
        Task<bool> DeleteUser(int id);

        Task<bool> IsUniqueEmail(string email);

        Task<bool> IsUniqueStaffCode(int StaffCode);

    }
}
