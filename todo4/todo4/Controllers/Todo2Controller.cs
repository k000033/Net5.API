using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;
using todo4.Parametes;
using todo4.services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Todo2Controller : ControllerBase
    {

        private readonly todoContext _todoContext;
        private readonly todoLostService _todoLostService;
        private readonly todoService2 _todoService2;
        public Todo2Controller(todoContext todoContext, todoLostService todoLostService,todoService2 todoService2 )
        {
            _todoContext = todoContext;
            _todoLostService = todoLostService;
            _todoService2 = todoService2;

        }

        // GET: api/<Todo2Controller>
        [Produces("application/json")]
        [HttpGet]
        public ActionResult Get([FromQuery] todoListParametes2 value)
        {
            //var result = _todoContext.TodoLists.Include(x=>x.InsertEmployee).Include(x=>x.UpdateEmployee).Include(x=>x.UploadFiles).Select(x => x);


            //if (!String.IsNullOrWhiteSpace(value.Name))
            //{
            //    result = result.Where(x => x.Name == value.Name).Select(x => x);
            //};

            //if(value.Order !=null)
            //{
            //    result = result.Where(x => x.Orders == Int32.Parse(value.Order)).Select(x => x);
            //}


            //if (value.MinOrder != null & value.MaxOrder != null)
            //{
            //    result = result.Where(x => x.Orders >= value.MinOrder & x.Orders <= value.MaxOrder).Select(x => x);
            //};



            //if (result==null)
            //{
            //    return NotFound("找不到");
            //}


            var result = _todoLostService.getTodoOne(value);
            if (result == null)
            {
                return NotFound("找不到");
            }

            return Ok(result);
        }

        // GET api/<Todo2Controller>/5
        [HttpGet("aaaa")]
        public ActionResult Get1()
        {
            var result =  _todoService2.getTodoList();

           return  Ok(result);
        }

        // POST api/<Todo2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Todo2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Todo2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        //public todoListGet listDto(TodoList a)
        //{
        //    List<UploadFileDTO> uploadFileDTOs = new List<UploadFileDTO>();

        //    foreach (var x in a.UploadFiles)
        //    {
        //        UploadFileDTO up = new UploadFileDTO
        //        {
        //            Name = x.Name,
        //            Src = x.Src,
        //            TodoId = x.TodoId,
        //            UploadFileId = x.UploadFileId

        //        };

        //        uploadFileDTOs.Add(up);
        //    }

        //    return new todoListGet
        //    {
        //        Name = a.Name,
        //        Enable = a.Enable,
        //        InsertEmployeeName = a.InsertEmployee.Name,
        //        UpdateEmployeeName = a.UpdateEmployee.Name,
        //        InsertTime = a.InsertTime,
        //        UpdateTime = a.UpdateTime,
        //        Orders = a.Orders,
                  
        //        UploadFiles = uploadFileDTOs

        //    };
        //}

    }
}
