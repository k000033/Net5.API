using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Dtos;
using Todo.Parameters;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TodoController(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _mapper = mapper;
        }

        // GET: api/<TodoController>
        [HttpGet]
        public IActionResult Get([FromQuery] TodoSelectParameter value)
        {
            var result = _todoContext.TodoLists
                .Include(a => a.InsertEmployee)
                .Include(a => a.UpdateEmployee)
                .Include(a => a.UploadFiles)
                .Select(a => a);

            if (!string.IsNullOrWhiteSpace(value.name))
            {
                result = result.Where(a => a.Name.IndexOf(value.name) > -1);
            }

            if (value.enable != null)
            {
                result = result.Where(a => a.Enable == value.enable);
            }

            if (value.InsertTime != null)
            {
                result = result.Where(a => a.InsertTime.Date == value.InsertTime);
            }

            if (value.minOrder != null && value.maxOrder != null)
            {
                result = result.Where(a => a.Orders >= value.minOrder && a.Orders <= value.maxOrder);
            }

            if (result == null || result.Count() <= 0)
            {
                return NotFound("找不到資源");
            }

            return Ok(result.ToList().Select(a => ItemToDto(a)));
        }

        // GET api/Todo/1f3012b6-71ae-4e74-88fd-018ed53ed2d3
        [HttpGet("{TodoId}")]
        public ActionResult<TodoListDto> GetOne(Guid TodoId)
        {
            var result = (from a in _todoContext.TodoLists
                          where a.TodoId == TodoId
                          select a)
                .Include(a => a.InsertEmployee)
                .Include(a => a.UpdateEmployee)
                .Include(a => a.UploadFiles)
                .SingleOrDefault();

            if (result == null)
            {
                return NotFound("找不到Id：" + TodoId + "的資料");
            }

            return ItemToDto(result);
        }

        [HttpGet("AutoMapper")]
        public IEnumerable<TodoListDto> GetAutoMapper([FromQuery] TodoSelectParameter value)
        {
            var result = _todoContext.TodoLists
                .Include(a => a.UpdateEmployee)
                .Include(a => a.InsertEmployee)
                .Include(a => a.UploadFiles)
                .Select(a => a);

            if (!string.IsNullOrWhiteSpace(value.name))
            {
                result = result.Where(a => a.Name.IndexOf(value.name) > -1);
            }

            if (value.enable != null)
            {
                result = result.Where(a => a.Enable == value.enable);
            }

            if (value.InsertTime != null)
            {
                result = result.Where(a => a.InsertTime.Date == value.InsertTime);
            }

            if (value.minOrder != null && value.maxOrder != null)
            {
                result = result.Where(a => a.Orders >= value.minOrder && a.Orders <= value.maxOrder);
            }

            var map = _mapper.Map<IEnumerable<TodoListDto>>(result);

            return map;
        }

        [HttpGet("AutoMapper/{id}")]
        public TodoListDto GetAutoMapper(Guid id)
        {
            var result = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).Include(a => a.UpdateEmployee)
                          .Include(a => a.InsertEmployee).SingleOrDefault();

            return _mapper.Map<TodoListDto>(result);
        }

        [HttpGet("From/{id}")]
        public dynamic GetFrom(string id, string id2, string id3, string id4)
        {
            List<dynamic> result = new List<dynamic>();

            result.Add(id);
            result.Add(id2);
            result.Add(id3);
            result.Add(id4);

            return result;
        }

        [HttpGet("GetSQL")]
        public IEnumerable<TodoList> GetSQL(string name)
        {
            string sql = "select * from todolist where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                sql = sql + " and name like N'%" + name + "%'";
            }

            var result = _todoContext.TodoLists.FromSqlRaw(sql);

            return result;
        }

        [HttpGet("GetSQLDto")]
        public IEnumerable<TodoListDto> GetSQLDto(string name)
        {
            string sql = @"SELECT [TodoId]
      ,a.[Name]
      ,[InsertTime]
      ,[UpdateTime]
      ,[Enable]
      ,[Orders]
      ,b.Name as InsertEmployeeName
      ,c.Name as UpdateEmployeeName
  FROM [TodoList] a
  join Employee b on a.InsertEmployeeId=b.EmployeeId
  join Employee c on a.UpdateEmployeeId=c.EmployeeId where 1=1";

            if (!string.IsNullOrWhiteSpace(name))
            {
                sql = sql + " and name like N'%" + name + "%'";
            }

            var result = _todoContext.ExecSQL<TodoListDto>(sql);

            return result;
        }

        // POST api/<TodoController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoListPostDto value)
        {


            TodoList insert = new TodoList
            {
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            foreach (var temp in value.UploadFiles)
            {
                insert.UploadFiles.Add(new UploadFile()
                {
                    Src = temp.Src,
                    Name = temp.Name
                });
            }


            _todoContext.TodoLists.Add(insert).CurrentValues.SetValues(value);
            _todoContext.SaveChanges();

            return CreatedAtAction(nameof(GetOne), new { TodoId = insert.TodoId }, insert);
        }

        [HttpPost("nofk")]
        public void Postnofk([FromBody] TodoListPostDto value)
        {

            TodoList insert = new TodoList
            {
                Name = value.Name,
                Enable = value.Enable,
                Orders = value.Orders,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };
            _todoContext.TodoLists.Add(insert);
            _todoContext.SaveChanges();

            foreach (var temp in value.UploadFiles)
            {
                UploadFile insert2 = new UploadFile
                {
                    Name = temp.Name,
                    Src = temp.Src,
                    TodoId = insert.TodoId
                };

                _todoContext.UploadFiles.Add(insert2);
            }

            _todoContext.SaveChanges();
        }

        [HttpPost("AutoMapper")]
        public void PostAutoMapper([FromBody] TodoListPostDto value)
        {
            var map = _mapper.Map<TodoList>(value);

            map.InsertTime = DateTime.Now;
            map.UpdateTime = DateTime.Now;
            map.InsertEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            map.UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");

            _todoContext.TodoLists.Add(map);
            _todoContext.SaveChanges();
        }

        [HttpPost("postSQL")]
        public void PostSQL([FromBody] TodoListPostDto value)
        {
            var name = new SqlParameter("name", value.Name);

            string sql = @"INSERT INTO [dbo].[TodoList]
           ([Name]
           ,[InsertTime]
           ,[UpdateTime]
           ,[Enable]
           ,[Orders]
           ,[InsertEmployeeId]
           ,[UpdateEmployeeId])
     VALUES
           (@name,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + value.Enable + "'," + value.Orders + ",'00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-000000000001')";

            _todoContext.Database.ExecuteSqlRaw(sql, name);
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TodoListPutDto value)
        {
            if (id != value.TodoId)
            {
                return BadRequest();
            }

            var update = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                update.UpdateTime = DateTime.Now;
                update.UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");

                _todoContext.TodoLists.Update(update).CurrentValues.SetValues(value);
                _todoContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        public void Put([FromBody] TodoListPutDto value)
        {
            //_todoContext.TodoLists.Update(value);
            //_todoContext.SaveChanges();

            // var update = _todoContext.TodoLists.Find(id);
            var update = (from a in _todoContext.TodoLists
                          where a.TodoId == value.TodoId
                          select a).SingleOrDefault();

            if (update != null)
            {
                update.UpdateTime = DateTime.Now;
                update.UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");

                update.Name = value.Name;
                update.Orders = value.Orders;
                update.Enable = value.Enable;
                _todoContext.SaveChanges();
            }
        }

        // PUT api/<TodoController>/5
        [HttpPut("AutoMapper/{id}")]
        public void PutAutoMapper(Guid id, [FromBody] TodoListPutDto value)
        {
            //_todoContext.TodoLists.Update(value);
            //_todoContext.SaveChanges();

            // var update = _todoContext.TodoLists.Find(id);
            var update = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                _mapper.Map(value, update);

                _todoContext.SaveChanges();
            }
        }

        [HttpPatch("{id}")]
        public void Patch(Guid id, [FromBody] JsonPatchDocument value)
        {
            var update = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                update.UpdateTime = DateTime.Now;
                update.UpdateEmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001");

                value.ApplyTo(update);

                _todoContext.SaveChanges();
            }
        }


        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {   
            var delete = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).Include(c => c.UploadFiles).SingleOrDefault();

            if (delete == null)
            {
                return NotFound("找不到刪除的資源");
            }

            _todoContext.TodoLists.Remove(delete);
            _todoContext.SaveChanges();

            return NoContent();
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("nofk/{id}")]
        public void NofkDelete(Guid id)
        {
            var child = from a in _todoContext.UploadFiles
                        where a.TodoId == id
                        select a;

            _todoContext.UploadFiles.RemoveRange(child);
            _todoContext.SaveChanges();

            var delete = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a).SingleOrDefault();

            if (delete != null)
            {
                _todoContext.TodoLists.Remove(delete);
                _todoContext.SaveChanges();
            }

        }

        [HttpDelete("list/{ids}")]
        public void Delete(string ids)
        {
            List<Guid> deleteList = JsonSerializer.Deserialize<List<Guid>>(ids);

            var delete = (from a in _todoContext.TodoLists
                          where deleteList.Contains(a.TodoId)
                          select a).Include(c => c.UploadFiles);

            _todoContext.TodoLists.RemoveRange(delete);
            _todoContext.SaveChanges();
        }



        private static TodoListDto ItemToDto(TodoList a)
        {
            List<UploadFileDto> updto = new List<UploadFileDto>();

            foreach (var temp in a.UploadFiles)
            {
                UploadFileDto up = new UploadFileDto
                {
                    Name = temp.Name,
                    Src = temp.Src,
                    TodoId = temp.TodoId,
                    UploadFileId = temp.UploadFileId
                };
                updto.Add(up);
            }


            return new TodoListDto
            {
                Enable = a.Enable,
                InsertEmployeeName = a.InsertEmployee.Name,
                InsertTime = a.InsertTime,
                Name = a.Name,
                Orders = a.Orders,
                TodoId = a.TodoId,
                UpdateEmployeeName = a.UpdateEmployee.Name,
                UpdateTime = a.UpdateTime,
                UploadFiles = updto
            };
        }
    }
}
