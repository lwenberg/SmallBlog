using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.DTOs.BlogDTOs;
using Infrastructure.Entities;

namespace Infrastructure.Mappers
{
    public class BlogMapperProfile : Profile
    {

        public BlogMapperProfile() 
        {
            CreateMap<CreateBlogDTO, Blog>();
            CreateMap<Blog, BlogDTO>();
        }
    }
}
