using ApplicationCore.Entities;
using Infrastructure;
using Infrastructure.Services.BlogServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Identity.Client;
using System.Reflection;

namespace Tests.Unit.BlogTests
{
    public class BlogServiceTests
    {
        [Fact]
        public async Task GetAllBlogs_ShouldReturnListOfBlogs()
        {
            var service = new BlogService();

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBlogById_ShouldReturnElementById() 
        {
            var service = new BlogService();

            var blogDto = await service.GetByIdAsync(1);

            Assert.NotNull(blogDto);
            Assert.Equal(1, blogDto.Id);
            Assert.Equal("Test", blogDto.Title);
        }

        [Fact]
        public async Task AddBlog_ShouldAddBlogToDataBase()
        {
            var service = new BlogService();

            var blog = new Blog
            {
                Id = 2,
                Title = "Test",
                Body = "Test",
                PubDate = DateTime.Now,
            };

            var result = await service.CreateAsync(blog);

            Assert.True(result);

            var blogAdded = await service.GetByIdAsync(blog.Id);

            Assert.NotNull(blogAdded);
            Assert.Equal(2, blogAdded.Id);
            Assert.Equal("Test", blogAdded.Title);
        }


        [Fact]
        public async Task UpdateBlog_ShouldUpdateBlog()
        {
            var service = new BlogService();

            var blog = new Blog
            {
                Id = 2,
                Title = "Test",
                Body = "Test",
                PubDate = DateTime.Now,
            };

            var result = await service.UpdateAsync(1, blog);

            Assert.True(result);

            var blogUpdated = await service.GetByIdAsync(blog.Id);
            Assert.NotNull(blogUpdated);
            Assert.Equal(2, blogUpdated.Id);
            Assert.Equal("Test", blogUpdated.Title);
            Assert.Equal("Test", blogUpdated.Body);
        }

        [Fact]
        public async Task DeleteBlog_ShouldDeleteBlogById() 
        {
            var service = new BlogService();
            Blog blog = new Blog { 
                Id = 2,
                Title = "Test",
                Body = "Test",
                PubDate = DateTime.Now,
            };

            var resultAdded = await service.CreateAsync(blog);
            var resultDeleted = await service.DeleteAsync(2);

            Assert.True(resultDeleted);

            var isDeleted = await service.GetByIdAsync(2);

            Assert.Null(isDeleted);
        }
    }
}
