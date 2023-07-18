using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Response
{
    public class StudentResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Faculty { get; set; }
        public int RollNo { get; set; }
        public int StudentCode { get; set; }
    }
}
