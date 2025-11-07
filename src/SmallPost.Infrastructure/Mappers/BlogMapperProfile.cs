using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.DTOs.BlogDTOs;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Mappers
{
    public class BlogMapperProfile : Profile
    {

        public BlogMapperProfile()
        {
            CreateMap<CreateBlogDTO, Blog>()
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom((src, dest, destMen, context) => context.Items["email"]));
            CreateMap<Blog, BlogDTO>();
        }
    }
}
