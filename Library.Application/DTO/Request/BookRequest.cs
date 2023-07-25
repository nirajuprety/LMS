using Library.Application.Manager.NewFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Request
{
    public class BookRequest
    {
        [Required(ErrorMessage = "Id is required and cannot be zero.")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        [RandomNumber]
        public string ISBN { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;


    }

}

