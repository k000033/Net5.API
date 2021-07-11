using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo5.DTO;
using todo5.Models;

namespace todo5.Abstract
{
    public class tolistEditAbatract:IValidatableObject
    {
        public string Name { get; set; }
        public bool? Enable { get; set; }
        public int? Orders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //IConfiguration a = null;
            //var s = a.GetConnectionString("TodoDatabase");

            todoContext _todoContext = (todoContext)validationContext.GetService(typeof(todoContext));
            var _name = _todoContext.TodoLists.Where(x => x.Name == Name).Select(x => x);

            if(this.GetType()==typeof(todoListPutDTO))
            {
                _name = _name.Where(x => x.Name != Name).Select(x => x);
            };

            if (_name.FirstOrDefault() != null)
            {
                yield return new ValidationResult("重複名稱", new string[] { "Name" });
            }

            yield return ValidationResult.Success;
        }
    }
}
