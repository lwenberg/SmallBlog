using Infrastructure.Entities;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories.BlogRespository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly WebContext _context;
        public BlogRepository(WebContext context)
        {
            _context = context;
        }

        public async Task<List<BlogDTO>> GetAllAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return blogs.Select(BlogMapper.ToDTO).ToList();
        }

        public async Task<BlogDTO?> GetByIdAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog is null) 
            {
                return null;
            }

            return BlogMapper.ToDTO(blog);
        }

        public async Task<bool> CreateAsync(BlogDTO blogDto, IdentityUser currentUser)
        {
            blogDto.Author = currentUser.Email;
            blogDto.PubDate = DateTime.Now;
            var blog = BlogMapper.ToEntity(blogDto);
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
