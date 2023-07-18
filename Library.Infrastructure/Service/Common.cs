using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class Common
    {
<<<<<<< HEAD
        public class ServiceResult<t>
        {
            public ResultStatus Status { get; set; }

            public string Message { get; set; }

            public t Data { get; set; }
        }
    }
    public enum ResultStatus
    {
        Ok,
        processError
    }
}

=======

        public class ServiceResult<t>
        {
            public StatusType Status { get; set; }
            public string Message { get; set; }
            public t Data { get; set; }
        }
    }
    public enum StatusType
    {
        Success,
        Failure,
>>>>>>> f88b524dea2df12d19388929a5d2f7ec55c67df7
    }
}
