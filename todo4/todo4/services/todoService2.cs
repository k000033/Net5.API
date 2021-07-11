using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;

namespace todo4.services
{
    public class todoService2
    {

        private readonly todoContext _todoContext;

        public todoService2(todoContext todoContext)
        {
            _todoContext = todoContext;
        }


        public List<todoListGet> getTodoList()
        {

            var result = _todoContext.TodoLists.Include(x=>x.InsertEmployee).Include(x=>x.UpdateEmployee).Include(x=>x.UploadFiles).Select(x => x);


            return result.Select(x => toToListGet(x)).ToList();
        }


        private static todoListGet toToListGet(TodoList todoList)
        {


            List<UploadFileDTO> uploadFileDTOs = new List<UploadFileDTO>();
            foreach(var tmp in todoList.UploadFiles)
            {
                UploadFileDTO up = new UploadFileDTO
                {
                    Name = tmp.Name,
                    Src = tmp.Src,
                    TodoId = tmp.TodoId,
                    UploadFileId = tmp.UploadFileId
                };

                uploadFileDTOs.Add(up);
            }

            return new todoListGet
            {
                Enable = todoList.Enable,
                InsertEmployeeName = todoList.InsertEmployee.Name,
                UpdateEmployeeName = todoList.UpdateEmployee.Name,
                InsertTime = todoList.InsertTime,
                Name = todoList.Name,
                Orders = todoList.Orders,
                UpdateTime = todoList.UpdateTime,
                UploadFiles = uploadFileDTOs
            };
        }

    }
}
