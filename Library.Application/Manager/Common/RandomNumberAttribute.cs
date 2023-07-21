using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Manager.NewFolder
{
    public class RandomNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is int intValue && intValue == 0)
            {
                Random random = new Random();
                intValue = random.Next(10000000,99999999);
                value = intValue;
            }
            return ValidationResult.Success;

        }
    }
}
