using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTOs;
using Infrastructure.Entities;

namespace Infrastructure.Mappers
{
    public class BlogMapper
    {
        public static BlogDTO ToDTO(Blog blog)
        {
            return new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Body = blog.Body,
                PubDate = blog.PubDate,
                Author = blog.Author
            };
        }

        public static Blog ToEntity(BlogDTO blogDto) 
        {
            return new Blog
            {
                Id = blogDto.Id,
                Title = blogDto.Title,
                Body = blogDto.Body,
                PubDate = blogDto.PubDate,
                Author = blogDto.Author
            };
        }
    }
}
