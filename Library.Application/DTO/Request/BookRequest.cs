using Library.Application.Manager.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Request
{
    public class BookRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [RandomNumber]
        public string ISBN { get; set; } // Declare the property

        public DateTime PublicationDate { get; set; }
        public string UpdatedBy { get; set; }

        
    }

}

