using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace Todo.Models
{
    public partial class UploadFile
    {
        public Guid UploadFileId { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public Guid TodoId { get; set; }
        
        [JsonIgnore]
        public virtual TodoList Todo { get; set; }
    }
}
