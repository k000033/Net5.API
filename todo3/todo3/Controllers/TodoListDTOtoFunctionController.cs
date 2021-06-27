using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo3.DTOS;
using todo3.Models;
using todo3.Parametes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListDTOtoFunctionController : ControllerBase
    {

        private readonly todoContext _todoContext;
        public TodoListDTOtoFunctionController(todoContext todoContext)
        {
            _todoContext = todoContext;
        }
        // GET: api/<TodoListDTOtoFunctionController>

        [HttpGet]
        public IActionResult Get([FromQuery] todolistparames2 value)
        {
            var result = _todoContext.TodoLists
                         .Include(x=>x.UpdateEmployee)
                         .Include(x=>x.InsertEmployee)
                         .Select(x =>x);

            if(!string.IsNullOrWhiteSpace(value.name))
            {
                result= result.Where(x => x.Name.Contains(value.name));
            };

            if(value.enable!=null)
            {
                result= result.Where(x => x.Enable == value.enable);
            };

            if(value.order!=null)
            {
                result= result.Where(x => x.Orders == Int32.Parse(value.order));
            };

            if(value.minOrder !=null && value.maxOrder!=null)
            {
                result= result.Where(x => x.Orders >= value.minOrder & x.Orders <= value.maxOrder);
            };

            return Ok(result.ToList().Select(x=>toListDTO(x)));
        }

        private static ToListDTO toListDTO(TodoList x)
        {
            return new ToListDTO
            {
                TodoId = x.TodoId,
                Name = x.Name,
                Enable = x.Enable,
                Orders = x.Orders,
                InsertTime = x.InsertTime,
                UpdateTime = x.UpdateTime,
                InsertEmployeeName = x.InsertEmployee.Name,
                UpdateEmployeeName = x.UpdateEmployee.Name
            };
        }
        // GET api/<TodoListDTOtoFunctionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TodoListDTOtoFunctionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoListDTOtoFunctionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoListDTOtoFunctionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
