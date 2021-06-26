using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo3.DTOS
{
    public class ToListDTO
    {
  
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }
        public string InsertEmployeeName { get; set; }
        public string UpdateEmployeeName { get; set; }
    }
}
