using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EIssueTable
    {
        public int Id { get; set; }
        public DateTime IssuedDate{ get; set; }
        public DateTime ReturnDate{ get; set; }
        public IssuedStatus IssuedStatus{ get; set; }
        //Foreign Key
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Member")]
        public int MemberId{ get; set; }
        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public double FineRate { get; set; }    
        public double FineAmount { get; set; }
        public bool IsDeleted { get; set; }


    }


}
