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
            var add = _factory.GetInstance<EStaff>();
            var staffInfo = await add.AddAsync(eStaff);
            return staffInfo.Id;

        }
        public async Task<bool> CreateLogin(ELogin log)
        {
            var add = _factory.GetInstance<ELogin>();
            var staffInfo = await add.AddAsync(log);
            return true;

        }

    }
}
