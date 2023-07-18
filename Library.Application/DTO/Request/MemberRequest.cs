using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Request
{
    public class MemberRequest
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public MemberType MemberType { get; set; }
        public int ReferenceId { get; set; }
        public int MemberCode { get; set; }
    }
}
