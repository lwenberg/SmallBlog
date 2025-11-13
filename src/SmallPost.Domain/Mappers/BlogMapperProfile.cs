using AutoMapper;
using SmallPost.Domain.DTOs.BlogDTOs;
using SmallPost.Domain.Entities;

namespace SmallPost.Domain.Mappers
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
