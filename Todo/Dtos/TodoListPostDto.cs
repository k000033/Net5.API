using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Todo.ValidationAttributes;

namespace Todo.Dtos
{    
    public class TodoListPostDto
    {
        [TodoName]
        public string Name { get; set; }
        public bool Enable { get; set; }
        public int Orders { get; set; }
        public List<UploadFilePostDto> UploadFiles { get; set; }

        public TodoListPostDto()
        {
            UploadFiles = new List<UploadFilePostDto>();
        }
    }
}
