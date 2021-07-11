using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;

namespace todo4.ValidationAttributes
{
    public class TodoNameAttribute : ValidationAttribute
    {   
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            todoContext todoContext = (todoContext)validationContext.GetService(typeof(todoContext));

            var name = (string)value;

            var fineName = from a in todoContext.TodoLists
                           where a.Name == name
                           select a;


            var dto = validationContext.ObjectInstance;

            if(dto.GetType()==typeof(todoListDTO))
            {
                var dtoUpdate = (todoListDTO)dto;
                fineName = fineName.Where(a => a.TodoId != dtoUpdate.TodoId);
            }


            if(fineName.FirstOrDefault()!=null)
            {
                return new ValidationResult("相同");
            };

            return ValidationResult.Success;

        }
    }
}
