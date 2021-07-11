using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo5.Abstract;

namespace todo5.DTO
{
    public class todoListPutDTO: tolistEditAbatract
    {
        public Guid TodoId { get; set; }
    }
}
