using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo4.DTO
{
    public class UploadFileDTO
    {
        public Guid UploadFileId { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public Guid TodoId { get; set; }
    }
}
