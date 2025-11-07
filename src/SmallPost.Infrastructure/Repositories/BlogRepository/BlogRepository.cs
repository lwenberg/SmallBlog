using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.DTOs.BlogDTOs;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using SmallPost.Infrastructure.Helpers;
using Microsoft.Identity.Client;

namespace Infrastructure.Repositories.BlogRespository
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
                .ProjectTo<BlogDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await PaginationListHelper<BlogDTO>.CreateAsync(blogs, totalItems, pageIndex, pageSize);
        }
    }
}
