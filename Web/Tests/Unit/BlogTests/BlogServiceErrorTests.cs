using Infrastructure.Services.BlogServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Unit.BlogTests
{
    public class BlogServiceErrorTests
    {
        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenBlogNotFound()
        {
            var service = new BlogService();

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenBlogNotFound()
        {
            var service = new BlogService();

            var blog = await service.GetByIdAsync(123); 

            Assert.Null(blog);
        }

        [Fact]
        public async Task UpdateBlog_ShouldReturnFalse_WhneBlogNotFound() 
        {
            var service = new BlogService();

            var result = await service.UpdateAsync(999, null!);

            Assert.False(result);
        }
    }
}
