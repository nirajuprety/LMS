using Library.Application.Manager.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Response
{
    public class BookResponse
    {
        public string Title { get; set; }
        public string Author { get; set; }
        [RandomNumber]
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;

    }
}
