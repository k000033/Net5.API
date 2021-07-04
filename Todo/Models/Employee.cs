using System;
using System.Collections.Generic;

#nullable disable

namespace Todo.Models
{
    public partial class Employee
    {
        public Employee()
        {
            TodoListInsertEmployees = new HashSet<TodoList>();
            TodoListUpdateEmployees = new HashSet<TodoList>();
        }

        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public Guid JobTitleId { get; set; }
        public Guid DivisionId { get; set; }

        public virtual Division Division { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<TodoList> TodoListInsertEmployees { get; set; }
        public virtual ICollection<TodoList> TodoListUpdateEmployees { get; set; }
    }
}
