using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using Infrastructure.Mappers;
using Infrastructure.DTOs.BlogDTOs;
using AutoMapper;

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

        public async Task<List<BlogDTO>> GetAllAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return blogs.Select(_mapper.Map<BlogDTO>).ToList();
        }

        public async Task<BlogDTO?> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog is null) 
            {
                return null;
            }

            return _mapper.Map<BlogDTO>(blog);
        }

        public async Task<bool> CreateAsync(CreateBlogDTO createBlogDto, IdentityUser currentUser)
        {
            createBlogDto.Author = currentUser.Email;
            var blog = _mapper.Map<Blog>(createBlogDto);
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
    }
}
