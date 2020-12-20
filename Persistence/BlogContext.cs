using System.Collections.Generic;
using EasyBlog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Persistence
{
    public sealed class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Source> Sources { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("easy_blog");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);
        }
    }
}