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
    public class TodoListDTO2Controller : ControllerBase
    {
        private readonly todoContext _todoContext;
        public TodoListDTO2Controller(todoContext todoContext)
        {
            _todoContext = todoContext;
        }

        // GET: api/<TodoListDTO2>
        //[HttpGet]
        //public IActionResult Get(string name, bool? enable, int? orders)
        //{
        //    var result = _todoContext.TodoLists
        //                             //.Where(x=>x.Name== "4414")
        //                             .Select(x => new ToListDTO
        //                             {
        //                                 TodoId = x.TodoId,
        //                                 Name = x.Name,
        //                                 Enable = x.Enable,
        //                                 Orders = x.Orders,
        //                                 InsertTime = x.InsertTime,
        //                                 UpdateTime = x.UpdateTime,
        //                                 InsertEmployeeName = x.InsertEmployee.Name,
        //                                 UpdateEmployeeName = x.UpdateEmployee.Name
        //                             });


        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        result = result.Where(x => x.Name == name); // 完全相同
        //        result = result.Where(x => x.Name.Contains(name)); // 有包含
        //    };

        //    if (enable != null)
        //    {
        //        result = result.Where(x => x.Enable == enable);
        //    };

        //    if (orders != null)
        //    {
        //        result = result.Where(x => x.Orders == orders);
        //    }

        //    if (result==null || result.Count()==0)
        //    {
        //        return NotFound("找不到");
        //    }

        //    return Ok( result);
        //}

        [HttpGet]
        public IActionResult Get([FromQuery]TodoListParames value)
        {
            var result = _todoContext.TodoLists
                                   //.Where(x=>x.Name== "4414")
                                   .Select(x => new ToListDTO
                                   {
                                       TodoId = x.TodoId,
                                       Name = x.Name,
                                       Enable = x.Enable,
                                       Orders = x.Orders,
                                       InsertTime = x.InsertTime,
                                       UpdateTime = x.UpdateTime,
                                       InsertEmployeeName = x.InsertEmployee.Name,
                                       UpdateEmployeeName = x.UpdateEmployee.Name
                                   });


            if (!string.IsNullOrWhiteSpace(value.name))
            {
                result = result.Where(x => x.Name == value.name); // 完全相同
                result = result.Where(x => x.Name.Contains(value.name)); // 有包含
            };

            if (value.enable != null)
            {
                result = result.Where(x => x.Enable == value.enable);
            };

            if (value.order != null)
            {
                result = result.Where(x => x.Orders == Int32.Parse( value.order));
            }

            if (result == null || result.Count() == 0)
            {
                return NotFound("找不到");
            }

            if (value.Minorder != null && value.Maxorder != null)
            {
                result = result.Where(x => x.Orders >= value.Minorder & x.Orders <= value.Maxorder);
            };

            return Ok(result);
        }

        // GET api/<TodoListDTO2>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var result = (from a in _todoContext.TodoLists
                          join b in _todoContext.Employees on a.InsertEmployeeId equals b.EmployeeId
                          join c in _todoContext.Employees on a.UpdateEmployeeId equals c.EmployeeId
                          where (a.TodoId == id)
                          select new ToListDTO
                          {
                              TodoId = a.TodoId,
                              Name = a.Name,
                              Enable = a.Enable,
                              Orders = a.Orders,
                              InsertTime = a.InsertTime,
                              UpdateTime = a.UpdateTime,
                              InsertEmployeeName = b.Name,
                              UpdateEmployeeName = c.Name
                          }).SingleOrDefault();

            return Ok(result);
        }

        // POST api/<TodoListDTO2>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoListDTO2>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoListDTO2>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
