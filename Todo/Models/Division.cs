using System;
using System.Collections.Generic;

#nullable disable

namespace Todo.Models
{
    public partial class Division
    {
        public Division()
        {
            Employees = new HashSet<Employee>();
        }

        public Guid DivisionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
