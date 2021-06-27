using System;
using System.Collections.Generic;

#nullable disable

namespace todo4.Models
{
    public partial class TodoList
    {
        public TodoList()
        {
            UploadFiles = new HashSet<UploadFile>();
        }

        public Guid TodoId { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }
        public Guid InsertEmployeeId { get; set; }
        public Guid UpdateEmployeeId { get; set; }

        public virtual Employee InsertEmployee { get; set; }
        public virtual Employee UpdateEmployee { get; set; }
        public virtual ICollection<UploadFile> UploadFiles { get; set; }
    }
}
