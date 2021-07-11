using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;

namespace todo4.Abstracts
{
    public abstract class todoListEditDtoAbstracts : IValidatableObject
    {
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }

        public virtual ICollection<UploadFileInsertDTO> UploadFiles { get; set; }




        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            todoContext _todoContext = (todoContext)validationContext.GetService(typeof(todoContext));


            var fileName = _todoContext.TodoLists.Where(x => x.Name == Name).Select(x => x);


            if(this.GetType()== typeof(UpdateDTO))
            {
                var dpUpdate = (UpdateDTO)this;
                fileName = fileName.Where(x => x.Name != Name).Select(x => x);
            }


            if (fileName.FirstOrDefault() != null)
            {

                yield return new ValidationResult("NAME", new string[] { "Name" });
            }


            if (InsertTime > UpdateTime)
            {
                yield return new ValidationResult("TIME", new string[] { "Time" });

            }


            yield return ValidationResult.Success;
        }
    }
}
