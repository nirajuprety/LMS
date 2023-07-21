using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IMemberService
    {
        Task<int> CreateMember(EMember member);
        Task<List<EMember>> GetMembers();
        Task<EMember> GetMemberById(int id);

        Task<bool> UpdateMember(EMember eStaff);

        Task<bool> DeleteMember(int id);
    }
}
