using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data;
using todo5.DTO;
using todo5.Parametes;
using todo5.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class todoList : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ConnectionService _connectionService;
        private readonly getTodoListToDto _getTodoListToDto;
        public todoList(IConfiguration configuration, ConnectionService connectionService, getTodoListToDto getTodoListToDto)
        {
            _configuration = configuration;
            _connectionService = connectionService;
            _getTodoListToDto = getTodoListToDto;
        }

        // GET: api/<todoList>
        [HttpGet]
        public ActionResult Get()
        {

            string ConnectionString = _configuration.GetConnectionString("TodoDatabase");
                string sql = @"SELECT T.[Name] AS Name 
                                     ,T.TodoId AS TodoId
                                     ,InsertTime
                               	     ,UpdateTime
                               	     ,Orders
                               	     ,Enable
                               	     ,I.Name AS InsertEmployeeName
                               	     ,U.Name AS UpdateEmployeeName
	                                 ,F.Name AS File_Name
	                                 ,F.Src  AS File_Src
	                                 ,F.TodoId As FILE_TodoId
                                 FROM TodoList T
                                 LEFT JOIN [dbo].[Employee] I
                                   ON T.InsertEmployeeId = I.EmployeeId
                                 LEFT JOIN [dbo].[Employee] U
                                   ON T.UpdateEmployeeId = U.EmployeeId
                                 LEFT JOIN [dbo].[UploadFile]　F
                                   ON T.TodoId = F.TodoId
                             ";
                SqlCommand com = new SqlCommand();
                com.CommandText = sql;

            var dt = _connectionService.DataTable(ConnectionString, com);
            var data = _getTodoListToDto.todoLostGetDTO(dt);

            return Ok(data);
        }

        // GET api/<todoList>/5
        [HttpGet("getOen")]
        public ActionResult GetOne(todoListGetParamete value)
        {
            string ConnectionString = _configuration.GetConnectionString("TodoDatabase");
         
   
                string sql = @"SELECT T.[Name] AS Name 
                                     ,T.TodoId AS TodoId
                                     ,InsertTime
                               	     ,UpdateTime
                               	     ,Orders
                               	     ,Enable
                               	     ,I.Name AS InsertEmployeeName
                               	     ,U.Name AS UpdateEmployeeName
	                                 ,F.Name AS File_Name
	                                 ,F.Src  AS File_Src
	                                 ,F.TodoId As FILE_TodoId
                                 FROM TodoList T
                                 LEFT JOIN [dbo].[Employee] I
                                   ON T.InsertEmployeeId = I.EmployeeId
                                 LEFT JOIN [dbo].[Employee] U
                                   ON T.UpdateEmployeeId = U.EmployeeId
                                 LEFT JOIN [dbo].[UploadFile]　F
                                   ON T.TodoId = F.TodoId
                                Where T.TodoId = @TodoId
                             ";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.Parameters.AddWithValue("@TodoId", value.TodoId);
            var dt = _connectionService.DataTable(ConnectionString, com);
            var s = _getTodoListToDto.todoLostGetDTO(dt);
            return Ok(s);
        }

        // POST api/<todoList>
        [HttpPost]
        public void Post([FromBody] todoListPostDTO value )
        {
            string connectString = _configuration.GetConnectionString("TodoDatabase");
           
           string sql = @"INSERT [dbo].[TodoList](Name
                       ,InsertTime
					   ,UpdateTime
					   ,Enable
					   ,Orders
					   ,InsertEmployeeId
					   ,UpdateEmployeeId)
                SELECT  @NAME
				       ,GETDATE()
					   ,GETDATE()
					   ,@ENABLE
					   ,@ORDERS
					   ,@INSERTEMPLOYEEID
					   ,@UPDATEEMPLOYEEID
                       ";

            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.Parameters.AddWithValue("@NAME", value.Name);
            com.Parameters.AddWithValue("@ENABLE", value.Enable);
            com.Parameters.AddWithValue("@ORDERS", value.Orders);
            com.Parameters.AddWithValue("@INSERTEMPLOYEEID", "00000000-0000-0000-0000-000000000001");
            com.Parameters.AddWithValue("@UPDATEEMPLOYEEID", "00000000-0000-0000-0000-000000000001");
            _connectionService.DataTable(connectString, com);

        }

        // PUT api/<todoList>/5
        [HttpPut]
        public void Put(todoListPutDTO value)
        {
            string connectString = _configuration.GetConnectionString("TodoDatabase");
            string sql = @"UPDATE [dbo].[TodoList]
                              SET Name = @NAME 
                                 ,Orders = @ORDERS
                           	     ,Enable = @ENABLE
                           	     ,UpdateTime = GETDATE()
                           	     ,UpdateEmployeeId = @UpdateEmployeeId 
                            WHERE TodoId = @TODOID";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.Parameters.AddWithValue("@TODOID", value.TodoId);
            com.Parameters.AddWithValue("@NAME", value.Name);
            com.Parameters.AddWithValue("@ENABLE", value.Enable);
            com.Parameters.AddWithValue("@ORDERS", value.Orders);
            com.Parameters.AddWithValue("@UPDATEEMPLOYEEID", "00000000-0000-0000-0000-000000000001");
            _connectionService.DataTable(connectString, com);
        }

        // DELETE api/<todoList>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
