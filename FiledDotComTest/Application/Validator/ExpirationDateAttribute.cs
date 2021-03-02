using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Application.Validator
{
    public class ExpirationDateAttribute : ValidationAttribute
    {
        public ExpirationDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
