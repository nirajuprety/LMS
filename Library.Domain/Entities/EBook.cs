using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EBook
    {
        //Primary Key
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set;} = DateTime.Now.ToUniversalTime();
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public BookStatus BookStatus { get; set; }

    }
}
