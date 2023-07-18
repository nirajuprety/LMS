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
    public class MemberService : IMemberService
    {
        private readonly IServiceFactory _factory = null;

        public MemberService(IServiceFactory factory)
        {
            _factory = factory;

        }

        public async Task<int> CreateMember(EMember member)
        {
            var add = _factory.GetInstance<EMember>();
            var memberInfo = await add.AddAsync(member);
            return memberInfo.Id;

        }
       
    }
}
