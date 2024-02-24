// Ignore Spelling: Blogging
// Ignore Spelling: Blog
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.DataBase
{
    public class BlogPlatformDbContext : DbContext
    {


        public DbSet<FullPost> posts { get; set; }

        public DbSet<Comments> comments {  get; set; }

        public BlogPlatformDbContext(DbContextOptions<BlogPlatformDbContext> options) : base(options)
        {
        }

      
    }
}
