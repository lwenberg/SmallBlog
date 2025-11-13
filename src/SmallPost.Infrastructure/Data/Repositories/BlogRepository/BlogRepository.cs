using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallPost.Domain.DTOs.BlogDTOs;
using SmallPost.Domain.Entities;
using SmallPost.Domain.Helpers;
using SmallPost.Domain.Services.BlogService;

namespace SmallPost.Infrastructure.Data.Repositories.BlogRepository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly WebContext _context;
        private readonly IMapper _mapper;
        public BlogRepository(WebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlogDTO?> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog is null)
            {
                return null;
            }

            return _mapper.Map<BlogDTO>(blog);
        }

        public async Task<bool> CreateAsync(CreateBlogDTO createBlogDto, IdentityUser currentUser)
        {
            var blog = _mapper.Map<CreateBlogDTO, Blog>(createBlogDto, opt => opt.Items["email"] = currentUser.Email);
            await _context.Blogs.AddAsync(blog);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateAsync(BlogDTO blog)
        {
            var result = await _context.Blogs.Where(b => b.Id == blog.Id).ExecuteUpdateAsync(
                    b => b.SetProperty(i => i.Title, blog.Title)
                    .SetProperty(i => i.Body, blog.Body)
                );

            return result != 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Blogs.Where(b => b.Id == id).ExecuteDeleteAsync();
            return result != 0;
        }

        public async Task<PaginationListHelper<BlogDTO>> GetPaginatedBlogsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var totalItems = await _context.Blogs.CountAsync(cancellationToken);
            var blogs = await _context.Blogs.AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(i => i.Id)
                .ProjectTo<BlogDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await PaginationListHelper<BlogDTO>.CreateAsync(blogs, totalItems, pageIndex, pageSize);
        }
    }
}
