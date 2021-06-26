using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_api2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo_api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // GET: api/<TodoController>
        private readonly DTODOMDFContext _dTODOMDFContext;
        
        public TodoController(DTODOMDFContext dTODOMDFContext)
        {
            _dTODOMDFContext = dTODOMDFContext;
        }
        [HttpGet("allList")]
        public ActionResult<IEnumerable<Tool>> Get()
        {
            return _dTODOMDFContext.Tools;
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Tool>> Get(Guid id)
        {
            var result =  _dTODOMDFContext.Tools;
            if(result == null)
            {
                return NotFound("找不到");
            };

            return result;

        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
