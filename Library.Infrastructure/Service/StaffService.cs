using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CreateStaff(EStaff eStaff)
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

        public async Task<List<EStaff>> GetAllStaff()
        {
            var service=  _factory.GetInstance<EStaff>();
            var staffInfo=await service.ListAsync();
            return staffInfo;
        }

        public async Task<EStaff> GetStaffById(int id)
        {
            var service = _factory.GetInstance<EStaff>();
            var staffInfo=await service.FindAsync(id);
            return staffInfo;
        }

        public Task<bool> UpdateStaff(EStaff eStaff)
        {

        }
    }
}
