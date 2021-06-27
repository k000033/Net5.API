using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.DTO;
using todo4.Models;
using todo4.Parametes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todoController : ControllerBase
    {
        private readonly todoContext _toContext;
        public todoController(todoContext todoContext)
        {
            _toContext = todoContext;
        }

        // GET: api/<todoController>
        [HttpGet]
        public ActionResult Get([FromQuery]todoListParametes value)
        {

            List<UploadFileDTO> uploadFileDTO = new List<UploadFileDTO>();

            foreach (var tmp in _toContext.UploadFiles)
            {
                UploadFileDTO up = new UploadFileDTO();
                up.Name = tmp.Name;
                up.Src = tmp.Src;
                up.TodoId = tmp.TodoId;
                uploadFileDTO.Add(up);
            };





            var result = _toContext.TodoLists.Select(x => new todoListDTO
            {
                Name = x.Name,
                Enable = x.Enable,
                Orders = x.Orders,
                InsertTime = x.InsertTime,
                UpdateTime = x.UpdateTime,
                TodoId = x.TodoId,
                UpdateEmployeeName = x.UpdateEmployee.Name,
                InsertEmployeeName = x.InsertEmployee.Name,
                UploadFiles = x.UploadFiles.Select(x => new UploadFileDTO
                {
                    Name = x.Name,
                    Src = x.Src,
                    TodoId = x.TodoId,
                    UploadFileId = x.UploadFileId
                }).ToList()

            }) ;

            if(!string.IsNullOrWhiteSpace(value.Name))
            {
                result = result.Where(x => x.Name.Contains(value.Name));
            };

            if(value.order!=null)
            {
                result = result.Where(x => x.Orders ==  Int32.Parse(value.order));
            };

            if(value.MinOrder!=null && value.MaxOrder!=null)
            {
                result = result.Where(x => x.Orders >= value.MinOrder & x.Orders <= value.MaxOrder);
            };

            return Ok(result);
        }

        // GET api/<todoController>/5
        [HttpGet("{id}")]
        public todoListDTO Get(Guid id)
        {
            //var result = _toContext.TodoLists.Where(x => x.TodoId == id)
            //             .Select(x => new todoListDTO
            //             {
            //                 Name = x.Name,
            //                 Enable = x.Enable,
            //                 Orders = x.Orders,
            //                 InsertTime = x.InsertTime,
            //                 UpdateTime = x.UpdateTime,
            //                 TodoId = x.TodoId,
            //                 UpdateEmployeeName = x.UpdateEmployee.Name,
            //                 InsertEmployeeName = x.InsertEmployee.Name,
            //                 UploadFiles = (from tmp in _toContext.UploadFiles
            //                                where (tmp.TodoId == x.TodoId)
            //                                select new UploadFileDTO
            //                                {
            //                                    Name = tmp.Name,
            //                                    Src = tmp.Src,
            //                                    TodoId = tmp.TodoId,
            //                                    UploadFileId = tmp.UploadFileId
            //                                }).ToList()
            //             }).SingleOrDefault();

            var result = _toContext.TodoLists.Where(x => x.TodoId == id)
                         .Select(x => new todoListDTO
                         {
                             Name = x.Name,
                             Enable = x.Enable,
                             Orders = x.Orders,
                             InsertTime = x.InsertTime,
                             UpdateTime = x.UpdateTime,
                             TodoId = x.TodoId,
                             UpdateEmployeeName = x.UpdateEmployee.Name,
                             InsertEmployeeName = x.InsertEmployee.Name,
                             UploadFiles = (_toContext.UploadFiles.Where(a => a.TodoId == x.TodoId).Select(a => new UploadFileDTO
                             {
                                 Name = a.Name,
                                 Src = a.Src,
                                 TodoId = a.TodoId,
                                 UploadFileId = a.UploadFileId
                             })).ToList()

                         }).SingleOrDefault() ;


            return result;
        }

        // POST api/<todoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<todoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<todoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
