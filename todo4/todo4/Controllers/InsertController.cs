using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertController : ControllerBase
    {
        private readonly todoContext _todoContext;
        public InsertController(todoContext todoContext)
        {
            _todoContext = todoContext;
        }

        // GET: api/<InsertController>
        [HttpGet]
        public ActionResult Get()
        {
            var result = _todoContext.TodoLists.Select(x => new todoListDTO {
               TodoId=x.TodoId,
               Enable=x.Enable,
               Name=x.Name,
               Orders=x.Orders,
               InsertTime=x.InsertTime,
               UpdateTime=x.UpdateTime,
               InsertEmployeeName=x.UpdateEmployee.Name,
               UpdateEmployeeName=x.InsertEmployee.Name,
               UploadFiles=(_todoContext.UploadFiles.Where(a=>a.TodoId==x.TodoId).Select(y=>new UploadFileDTO { 
               Name = y.Name,
               Src=y.Src,
               TodoId = y.TodoId
               })).ToList()
            });


            return Ok(result);
        }

        // GET api/<InsertController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //這是有設主建
        // POST api/<InsertController>
        [HttpPost]
        public void Post([FromBody] TodoList value)
        {
            TodoList todoList = new TodoList
            {
                Name = value.Name,
                Orders = value.Orders,
                Enable = value.Enable,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UploadFiles = value.UploadFiles
            };

            _todoContext.TodoLists.Add(todoList);
            _todoContext.SaveChanges();
        }

        //這是沒有設主建
        [HttpPost("Insert_V2")]
        public void Post_insert_V2([FromBody] TodoList value)
        {
            TodoList todoList = new TodoList
            {
                Name = value.Name,
                Orders = value.Orders,
                Enable = value.Enable,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            };

            _todoContext.TodoLists.Add(todoList); // _todoList 已經取得 todoId
            _todoContext.SaveChanges();


            foreach(var tmp in value.UploadFiles)
            {
                UploadFile uploadFile = new UploadFile
                {
                    Name = tmp.Name,
                    Src = tmp.Src,           
                    TodoId = todoList.TodoId
                };

                _todoContext.UploadFiles.Add(uploadFile);
                _todoContext.SaveChanges();
            };

        }



        [HttpPost("Insert_V3")]
        public void Post_insert_V3([FromBody] todoListInsertDTO value)
        {
            List<UploadFile> uploadFiles = new List<UploadFile>();
            foreach (var tmp in value.UploadFiles)
            {
                UploadFile  uploadFile = new UploadFile
                {
                    Name = tmp.Name,
                    Src = tmp.Src,
                };

                uploadFiles.Add(uploadFile);
            };


            TodoList todoList = new TodoList
            {
                Name = value.Name,
                Orders = value.Orders,
                Enable = value.Enable,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UploadFiles = uploadFiles
            };

            _todoContext.TodoLists.Add(todoList); // _todoList 已經取得 todoId
            _todoContext.SaveChanges();


            //foreach (var tmp in value.UploadFiles)
            //{
            //    UploadFile uploadFile = new UploadFile
            //    {
            //        Name = tmp.Name,
            //        Src = tmp.Src,
            //        TodoId = todoList.TodoId
            //    };

            //    _todoContext.UploadFiles.Add(uploadFile);
            //    _todoContext.SaveChanges();
            //};

        }

        //[HttpPost("UploadFiles")]
        //public string Post([FromBody] TodoList value)
        //{


        //    TodoList todoList = new TodoList
        //    {
        //        Name = value.Name,
        //        Enable = value.Enable,
        //        Orders = value.Orders,
        //        InsertTime = DateTime.Now,
        //        UpdateTime = DateTime.Now,
        //        InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        //        UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        //        UploadFiles = value.UploadFiles
        //    };

        //    _todoContext.TodoLists.Add(value);
        //    _todoContext.SaveChanges();
        //    return "OK";
        //}

        // PUT api/<InsertController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InsertController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
