using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class StaffService : IStaffService
    {
        private readonly IServiceFactory _factory = null;

        public StaffService(IServiceFactory factory)
        {
            _factory = factory;

        }

        public async Task<int> AddStaff(EStaff eStaff)
        {
            var service = _factory.GetInstance<EStaff>();
            var staffInfo = await service.AddAsync(eStaff);
            return staffInfo.Id;

        }
        public async Task<bool> CreateLogin(ELogin log)
        {
            var service = _factory.GetInstance<ELogin>();
            var staffInfo = await service.AddAsync(log);
            return true;

        }
        public async Task<bool> UpdateUser(ELogin login)
        {
            var user = _factory.GetInstance<ELogin>();
            var staffInfo = await user.UpdateAsync(login);
            return true;
        }

        public async Task<List<EStaff>> GetAllStaff()
        {
            var service = _factory.GetInstance<EStaff>();
            var staffInfo = await service.ListAsync();
            return staffInfo;
        }

        public async Task<EStaff> GetStaffById(int id)
        {
            var service = _factory.GetInstance<EStaff>();
            var staffInfo = service.ListAsync().Result.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
            return staffInfo;
        }

        public async Task<bool> UpdateStaff(EStaff eStaff)
        {
            var service = _factory.GetInstance<EStaff>();
            var result = await service.FindAsync(eStaff.Id);


            if (result.IsDeleted == true)
            {
                return false;
            }
            result.Id = eStaff.Id;
            result.Username = eStaff.Username;
            result.Password = eStaff.Password;
            result.Name = eStaff.Name;
            result.Email = eStaff.Email;
            result.UpdatedDate = DateTime.Now.ToUniversalTime();
            result.CreatedDate =eStaff.CreatedDate.ToUniversalTime();
            result.StaffCode = eStaff.StaffCode;
            result.StaffType = eStaff.StaffType;

            await service.UpdateAsync(result);

            return true;
        }



        public async Task<bool> DeleteStaff(int id)
        {
            var service = _factory.GetInstance<EStaff>();
            var result = await service.FindAsync(id);
            //await service.RemoveAsync(result);

            if (result != null)
            {
                result.IsDeleted = true;
                result.IsActive = false;
                await service.UpdateAsync(result);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var service = _factory.GetInstance<ELogin>();
            var result = await service.FindAsync(id);
            await service.RemoveAsync(result);

            //if (result != null)
            //{
            //    result.IsDeleted = true;
            //    result.IsActive = false;
            //    await service.UpdateAsync(result);
            //    return true;
            //}
            return true;
        }

        public async Task<bool> IsUniqueEmail(string email)
        {
            var staff = await _factory.GetInstance<EStaff>().ListAsync();
            var result = staff.Where(staff => staff.Email == email).Any();
            return result;
        }

        public async Task<bool> IsUniqueStaffCode(int StaffCode)
        {
            var staff = await _factory.GetInstance<EStaff>().ListAsync();
            var result = staff.Where(staff => staff.StaffCode == StaffCode).Any();
            return result;
        }

    }
}

