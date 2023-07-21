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
        public async Task<EMember> GetMemberById(int id)
        {
            try
            {
                var service = _factory.GetInstance<EMember>();
                var user = await service.FindAsync(id);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<List<EMember>> GetMembers()
        {
            try
            {
                var service = _factory.GetInstance<EMember>();
                var result = await service.ListAsync();
                return result;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<bool> UpdateMember(EMember member)
        {
            var service = _factory.GetInstance<EMember>();
            var result = await service.FindAsync(member.Id);

            result.Id = member.Id;
            result.FullName = member.FullName;
            result.Email = member.Email;
            result.MemberType = member.MemberType;
            result.ReferenceId = member.ReferenceId;
            result.MemberCode = member.MemberCode;
            
            await service.UpdateAsync(result);

            return true;

        }
        public async Task<bool> DeleteMember(int id)
        {
            var service = _factory.GetInstance<EMember>();
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
    }
}
