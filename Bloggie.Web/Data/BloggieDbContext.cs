using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BloggieDbContext : DbContext
    {
        // Contructor
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }

        // Creating Enties i.e Database Tables

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
    }
}
