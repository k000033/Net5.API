using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Dtos;
using Todo.Models;

namespace Todo.ValidationAttributes
{
    public class TodoNameAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TodoContext _todoContext = (TodoContext)validationContext.GetService(typeof(TodoContext));

            var name = (string)value;

            var findName = from a in _todoContext.TodoLists
                           where a.Name == name
                           select a;

            var dto = validationContext.ObjectInstance;

            if (dto.GetType() == typeof(TodoListPutDto))
            {
                var dtoUpdate = (TodoListPutDto)dto;
                findName = findName.Where(a => a.TodoId != dtoUpdate.TodoId);
            }

            if (findName.FirstOrDefault() != null)
            {
                return new ValidationResult("已存在相同的代辦事項");
            }

            return ValidationResult.Success;
        }
    }
}
