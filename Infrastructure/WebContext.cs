using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Infrastructure.Entities;

namespace Infrastructure
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options)
         : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
