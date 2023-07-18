using Library.Application.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Interface
{
    public interface IMemberManager
    {
        Task<ServiceResult<bool>> CreateMember(MemberRequest request);
    }
}
