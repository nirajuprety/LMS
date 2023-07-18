using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Manager.Interface
{
    public class ILoginManager
    {
        public interface ILoginManager
        {
            Task<ServiceResult<bool>> LoginUser(LoginRequest request);
            Task<ServiceResult<SignUpResponse>> UserDetails(string email);

            Task<string> GetToken(LoginRequest request);

        }
    }
}
