using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public  class EStaff
    {
        [Key]
        public int Id{ get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime UpdatedDate { get; set; }
        public int StaffCode{ get; set;}
        public StaffType StaffType { get; set; } 

    }    
    }

