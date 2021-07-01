using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo4.Models;

namespace todo4.DTO
{
    public class todoListInsertDTO
    {
   
        public string Name { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }

        public virtual ICollection<UploadFileInsertDTO> UploadFiles { get; set; }
    }
}
