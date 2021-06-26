using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo3.DTOS;
using todo3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToListDtoController : ControllerBase
    {

        private readonly todoContext _todoContent;
        public ToListDtoController(todoContext todoContext)
        {
            _todoContent = todoContext;
        }

        // GET: api/<ToListDto>
        [HttpGet]
        public IEnumerable<ToListDTO> Get()
        {
            var result = _todoContent.TodoLists.Include(a => a.InsertEmployee).Include(a => a.UpdateEmployee)
                        .Select(x => new ToListDTO
                        {
                            Name = x.Name,
                            Enable = x.Enable,
                            Orders = x.Orders,
                            InsertEmployeeName = x.InsertEmployee.Name,
                            UpdateEmployeeName = x.UpdateEmployee.Name,
                            InsertTime = x.InsertTime,
                            UpdateTime = x.UpdateTime
                        });
            return result;
        }

        // GET api/<ToListDto>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ToListDto>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ToListDto>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ToListDTO value)
        {
            
        }

        // DELETE api/<ToListDto>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
