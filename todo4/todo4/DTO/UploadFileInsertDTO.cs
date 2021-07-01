using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo4.DTO
{
    public class UploadFileInsertDTO
    {
        public string Name { get; set; }
        public string Src { get; set; }
        public Guid TodoId { get; set; }
    }
}
