using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoItemsController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }
        // GET: api/<TodoItemsController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoList>> Get()
        {
            return _todoContext.TodoLists.ToList();
        }

        // GET api/<TodoItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoList> Get(Guid id)
        {
            var reault = _todoContext.TodoLists.Find(id);
            if(reault ==null)
            {
                return NotFound("找不到答案");
            };

            return reault;
        }

        // POST api/<TodoItemsController>
        [HttpPost]
        public ActionResult<TodoList> Post([FromBody] TodoList value)
        {
            _todoContext.TodoLists.Add(value);
            _todoContext.SaveChanges();

           return CreatedAtAction(nameof(Get), new { id = value.TodoId }, value);

        }

        // PUT api/<TodoItemsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TodoList value)
        {
             if(id!=value.TodoId)
            {
                return BadRequest();
            };

            _todoContext.Entry(value).State = EntityState.Modified;

            try
            {
                _todoContext.SaveChanges();
            }catch
            {
                if(!_todoContext.TodoLists.Any(x=>x.TodoId==id))
                {
                    return NotFound("找不到");
                }else
                {
                    return StatusCode(500, "存取發生錯誤");
                };


            }

            return NoContent();

        }

        // DELETE api/<TodoItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
