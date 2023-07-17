using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public interface IServiceFactory
    {
        IServiceReprository<t> GetInstance<t>() where t : class;

        void BeginTransaction();

        void RollBack();

        void CommitTransaction();
    }
}
