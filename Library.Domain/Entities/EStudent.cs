using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class EStudent
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email{ get; set; }
        public string Faculty{ get; set; }
        public int RollNo { get; set; }
        public int StudentCode{ get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Today.ToUniversalTime();
        public string UpdatedBy { get; set; }


    }
}
