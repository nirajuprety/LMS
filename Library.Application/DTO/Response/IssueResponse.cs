using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Response
{
    public class IssueResponse
    {
        public int Id { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int IssuedStatus { get; set; }
        public int BookId { get; set; }

        public int MemberId { get; set; }
        public int StaffId { get; set; }

        public double FineRate { get; set; }
        public double FineAmount { get; set; }

        public bool IsDeleted { get; set; }
    }
}
