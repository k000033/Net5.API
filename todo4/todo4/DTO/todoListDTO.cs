using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.Models;

namespace todo4.DTO
{
    public class todoListDTO
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }
        public string InsertEmployeeName { get; set; }
        public string UpdateEmployeeName { get; set; }

        public virtual ICollection<UploadFileDTO> UploadFiles { get; set; }

    }
}
