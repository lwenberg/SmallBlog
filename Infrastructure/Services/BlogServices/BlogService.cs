using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;

namespace Infrastructure.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly WebContext _context;

        public BlogService(WebContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateAsync(Blog blog)
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
