# nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace validate
{
    public class LessDateAttribute:ValidationAttribute
    {
        public LessDateAttribute():base("{0}Date should less than current date")
        {
            
        }
        public override bool IsValid(object value)
        {
            DateTime Value = Convert.ToDateTime(value);
            if(Value <= DateTime.Now)
               return true;
            else
               return false;
        }    
    }
}