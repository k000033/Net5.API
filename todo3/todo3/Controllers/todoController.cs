using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todoController : ControllerBase
    {
        private readonly todoContext _todoContent;
        public todoController(todoContext todoContext)
        {
            _todoContent = todoContext;
        }


        // GET: api/<todoController>
        [HttpGet]
        public ActionResult<IEnumerable<Tool>> Get()
        {
            return _todoContent.Tools;
        }


        //單一條件
        // GET api/<todoController>/5
        [HttpGet("{id}")]
        public ActionResult<Tool> Get(Guid id)
        {
            var result = _todoContent.Tools.Find(id);

            if (result == null)
            {
                return NotFound("找不到");

            };

            return result;
        }

        //多條件
        // GET api/<todoController>/5
        [HttpGet("{id}/{_Name}")]
        public ActionResult<Tool> Get(Guid id,string _Name)
        {
            //var result = _todoContent.Tools.Find(id);
           //Landa
            //var result = _todoContent.Tools.Where(x => x.DivisionId == id).Where(x => x.Name == _Name).SingleOrDefault();
            //LinQ
            var result = from a in _todoContent.Tools
                         where (a.DivisionId == id & a.Name == _Name)                        
                         select a;

            if (result == null)
            {
                return NotFound("找不到");

            };

            return result.SingleOrDefault();
        }

        [HttpPut("todolist/{id}")]
        public ActionResult<TodoList> Put(Guid id, [FromBody] TodoList value)
        {
            //var result = _todoContent.TodoLists.Where(x => x.Name == "去睡覺").SingleOrDefault();
            //return result;
            value.InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            //value.UpdateEmployeeId = Guid.Parse("59308743-99E0-4D5A-B611-B0A7FACAF21E");
            //value.UpdateTime = DateTime.Now;
            //value.InsertTime = DateTime.Now;

            //_todoContent.TodoLists.Update(value);
            //_todoContent.SaveChanges();

            //return NoContent();

            var result = _todoContent.TodoLists.Where(x => x.TodoId == id).SingleOrDefault();

            result.InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            result.UpdateEmployeeId = Guid.Parse("59308743-99E0-4D5A-B611-B0A7FACAF21E");
            result.UpdateTime = DateTime.Now;
            result.InsertTime = DateTime.Now;

            result.Name = value.Name;
            result.Enable = value.Enable;
            result.Orders = value.Orders;

            _todoContent.TodoLists.Update(result);
            _todoContent.SaveChanges();

            return NoContent();

        }




        // POST api/<todoController>
        [HttpPost]
        public ActionResult<Tool> Post([FromBody] Tool value)
        {
            _todoContent.Tools.Add(value);
            _todoContent.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = value.DivisionId }, value);
        }

        // PUT api/<todoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Tool value)
        {
            if (id != value.DivisionId)
            {
                return BadRequest();
            };

            _todoContent.Entry(value).State = EntityState.Modified;

            try
            {
                _todoContent.SaveChanges();
            }
            catch
            {
                if (!_todoContent.Tools.Any(x => x.DivisionId == id))
                {
                    return NotFound("找不到");
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                };
            };

            return NoContent();
        }

        // DELETE api/<todoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _todoContent.Tools.Find(id);

            if(result == null)
            {
                return NotFound("找不到");
            };

            _todoContent.Tools.Remove(result);
            _todoContent.SaveChanges();
            return NoContent();

        }
    }
}
