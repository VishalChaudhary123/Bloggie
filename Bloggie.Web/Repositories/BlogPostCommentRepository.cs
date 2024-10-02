using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext _db;

        public BlogPostCommentRepository(BloggieDbContext db)
        {
            _db = db;
        }
        public async Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment)
        {
            await _db.BlogPostComments.AddAsync(blogPostComment);
            await _db.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogId(Guid blogPostId)
        {
          return  await _db.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
