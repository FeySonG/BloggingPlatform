// Ignore Spelling: Blogging
// Ignore Spelling: Blog
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.DataBase
{
    public class BlogPlatformDbContext : DbContext
    {


        public DbSet<FullPost> Posts { get; set; }

        public DbSet<Comments> Comments {  get; set; }

        public BlogPlatformDbContext(DbContextOptions<BlogPlatformDbContext> options) : base(options)
        {
        }

      
    }
}
