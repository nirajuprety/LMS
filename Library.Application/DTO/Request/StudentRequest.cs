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
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        public string Faculty { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Should be positive number")]
        public int RollNo { get; set; }

        public int StudentCode { get; set; }
        public string UpdatedBy { get; set; } = "StudentID";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
