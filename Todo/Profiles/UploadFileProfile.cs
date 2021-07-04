using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Dtos;
using Todo.Models;

namespace Todo.Profiles
{
    public class UploadFileProfile : Profile
    {
        public UploadFileProfile()
        {
            CreateMap<UploadFile, UploadFileDto>();
            CreateMap<UploadFilePostDto, UploadFile>();            
        }
    }
}
