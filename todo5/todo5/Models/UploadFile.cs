using System;
using System.Collections.Generic;

#nullable disable

namespace todo5.Models
{
    public partial class UploadFile
    {
        public Guid UploadFileId { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public Guid TodoId { get; set; }

        public virtual TodoList Todo { get; set; }
    }
}
