using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;
using todo4.Parametes;

namespace todo4.services
{
    public class todoLostService
    {
        private readonly todoContext _todoContext;
        public todoLostService(todoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public List<todoListGet> getTodoOne(todoListParametes2 value)
        {
            var result = _todoContext.TodoLists.Include(x => x.InsertEmployee).Include(x => x.UpdateEmployee).Include(x => x.UploadFiles).Select(x => x);


            if (!String.IsNullOrWhiteSpace(value.Name))
            {
                result = result.Where(x => x.Name == value.Name).Select(x => x);
            };

            if (value.Order != null)
            {
                result = result.Where(x => x.Orders == Int32.Parse(value.Order)).Select(x => x);
            }


            if (value.MinOrder != null & value.MaxOrder != null)
            {
                result = result.Where(x => x.Orders >= value.MinOrder & x.Orders <= value.MaxOrder).Select(x => x);
            };



            return result.ToList().Select(x => listDto(x)).ToList();
        }


        public todoListGet listDto(TodoList a)
        {
            List<UploadFileDTO> uploadFileDTOs = new List<UploadFileDTO>();

            foreach (var x in a.UploadFiles)
            {
                UploadFileDTO up = new UploadFileDTO
                {
                    Name = x.Name,
                    Src = x.Src,
                    TodoId = x.TodoId,
                    UploadFileId = x.UploadFileId

                };

                uploadFileDTOs.Add(up);
            }

            return new todoListGet
            {
                Name = a.Name,
                Enable = a.Enable,
                InsertEmployeeName = a.InsertEmployee.Name,
                UpdateEmployeeName = a.UpdateEmployee.Name,
                InsertTime = a.InsertTime,
                UpdateTime = a.UpdateTime,
                Orders = a.Orders,

                UploadFiles = uploadFileDTOs

            };
        }

    }
}
