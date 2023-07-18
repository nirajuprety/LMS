using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Request
{
    public class StudentRequest
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Faculty { get; set; }
        public int RollNo { get; set; }
        public int StudentCode { get; set; }
        public string UpdatedBy { get; set; } = "StudentID";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
