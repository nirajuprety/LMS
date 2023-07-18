using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EBook
    {
        //Primary Key
        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedBy { get; set; } = string.Emtpy;
        public DateTime CreatedDate { get; set; }   

    }
}
