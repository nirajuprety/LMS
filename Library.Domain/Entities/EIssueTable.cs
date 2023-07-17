using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EIssueTable
    {
        public int Issue_Id { get; set; }
        public DateTime Issued_Date{ get; set; }
        public DateTime Return_Date{ get; set; }
        public IssuedStatus IssuedStatus{ get; set; }
        //Foreign Key
        public int Book_Id { get; set; }
        public int Student_Id { get; set; }
        public int Staff_Id { get; set; }
    }

   
}
