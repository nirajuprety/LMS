using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Response
{
    public class MemberResponse
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string MemberType { get; set; }
        public int ReferenceId { get; set; }
        public int MemberCode { get; set; }
    }
}
