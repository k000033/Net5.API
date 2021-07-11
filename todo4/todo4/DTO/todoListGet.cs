using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.Models;
using todo4.ValidationAttributes;

namespace todo4.DTO
{
    public class todoListGet
    {
        //[TodoNameAttribute]
        [StringLength(2, ErrorMessage = "錯誤")]
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
