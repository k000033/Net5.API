using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;

namespace todo4.ValidationAttributes
{
    public class insertTimeAttributes:ValidationAttribute
    {

        private string msg;
        public string msg2;
        public insertTimeAttributes(string _msg = "錯誤")
        {
            msg = _msg;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var s = (UpdateDTO)value;

            if(s.InsertTime>s.UpdateTime)
            {
                return new ValidationResult(msg2, new string[] { "time"});
            }

            return ValidationResult.Success;

        }
    }
}
