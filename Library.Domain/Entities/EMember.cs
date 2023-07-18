using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EMember
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        //here address is change to email
        public string Email { get; set; }
        public MemberType MemberType { get; set; }
        public int ReferenceId{ get; set; }
        public int MemberCode{ get; set; }
    }
}
