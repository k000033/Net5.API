using System;
using System.Collections.Generic;

#nullable disable

namespace Todo.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            Employees = new HashSet<Employee>();
        }

        public Guid JobTitleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
