using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.Abstracts;
using todo4.Models;
using todo4.ValidationAttributes;

namespace todo4.DTO
{

    //[insertTimeAttributes(msg2 ="1111")]
    public class UpdateDTO:todoListEditDtoAbstracts
    {
        public Guid TodoId { get; set; }

    }
}
