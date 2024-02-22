using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.DataBase
{
    public class BlogPlatformDB : DbContext
    {


        public DbSet<Post> Posts { get; set; }

        public DbSet<Comments> comments {  get; set; }

        public BlogPlatformDB(DbContextOptions<BlogPlatformDB> options) : base(options)
        {
        }

      
    }
}
