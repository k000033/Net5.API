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
    public class UploadFileController : ControllerBase
    {

        private readonly todoContext _todoContent;
        public UploadFileController(todoContext todoContext)
        {
            _todoContent = todoContext;
        }
        // GET: api/<UploadFileController>

        [HttpGet]
        public ActionResult Get()
        {
            var result = _todoContent.UploadFiles.Select(x => x);

            return Ok(result);
        }


        [HttpGet("TodoList/{TodoId}")]
        public ActionResult Get(Guid TodoId)
        {
            if (!_todoContent.UploadFiles.Any(x => x.TodoId == TodoId))
            {
                return NotFound("找不到檔案");
            };

            var result = _todoContent.UploadFiles.Where(x => x.TodoId == TodoId)
                        .Select(x => new UploadFileDTO
                        {
                            Name = x.Name,
                            Src = x.Src,
                            TodoId = x.TodoId,
                            UploadFileId = x.UploadFileId
                        });

            if (result == null || result.Count() <= 0)
            {
                return NotFound("沒有檔案");
            };

            return Ok(result);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<UploadFileController>/5
        [HttpGet("TodoList/{TodoId}/file/{FileId}")]
        public ActionResult Get(Guid TodoId,Guid FileId)
        {
            if (!_todoContent.UploadFiles.Any(x => x.TodoId == TodoId))
            {
                return NotFound("找不到檔案");
            };

            var result = _todoContent.UploadFiles.Where(x => x.TodoId == TodoId)
            .Where(x=>x.TodoId == TodoId & x.UploadFileId == FileId)
            .Select(x => new UploadFileDTO
            {
                Name = x.Name,
                Src = x.Src,
                TodoId = x.TodoId,
                UploadFileId = x.UploadFileId
            }).SingleOrDefault();

            if (result == null)
            {
                return NotFound("沒有檔案");
            };

            return Ok(result);
        }

        // POST api/<UploadFileController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UploadFileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploadFileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
