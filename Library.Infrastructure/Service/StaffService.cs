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
    public class StaffService:IStaffService
    {
        private readonly IServiceFactory _factory = null;

        public StaffService(IServiceFactory factory)
        {
            _factory = factory;

        }

        public async Task<bool>CreateStaff(EStaff eStaff)
        {
            var add = _factory.GetInstance<EStaff>();
            await add.AddAsync(eStaff);
            return true;
        }
    }
}
