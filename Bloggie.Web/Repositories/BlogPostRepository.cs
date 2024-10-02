using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _db;

        public BlogPostRepository(BloggieDbContext db)
        {
            _db = db;
        }
        public async Task<BlogPost> AddBlogAsync(BlogPost blog)
        {
           await  _db.BlogPosts.AddAsync(blog);
           await  _db.SaveChangesAsync();

            return blog;
        }

        public async Task<BlogPost?> DeleteBlogAsync(Guid id)
        {
            var existingBlog = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlog != null) 
            {
               _db.BlogPosts.Remove(existingBlog);
                await _db.SaveChangesAsync();
                return existingBlog;
            }
            return null;

        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogAsync()
        {
            return await _db.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogByIdAsync(Guid id)
        {
           var existingBlog =  await  _db.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync(p => p.Id == id);
            if (existingBlog != null) 
            {
                return existingBlog;
            }
            return null;
        }

        public async Task<BlogPost?> GetBlogByUrlHandleAsync(string urlHandle)
        {
           return await _db.BlogPosts.Include(t => t.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateBlogAsync(BlogPost blog)
        {
            var existingBlog = await _db.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blog.Id);
            if (existingBlog != null) 
            {
               existingBlog.Heading = blog.Heading;
               existingBlog.PageTitle = blog.PageTitle;
               existingBlog.Content = blog.Content;
                existingBlog.Author = blog.Author;
                existingBlog.PublishedDate = blog.PublishedDate;
                existingBlog.ShortDescription = blog.ShortDescription;
                existingBlog.FeaturedImageUrl = blog.FeaturedImageUrl;
                existingBlog.UrlHandle = blog.UrlHandle;
                existingBlog.Tags = blog.Tags;

              await  _db.SaveChangesAsync();
                return existingBlog;
            }
            return null ;
        }
    }
}
