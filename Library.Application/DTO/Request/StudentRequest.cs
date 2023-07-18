using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Request
{
    public class StudentRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Faculty { get; set; }
        [Required]
        public int RollNo { get; set; }

        [Required]
        public int StudentCode { get; set; }
    }
}
