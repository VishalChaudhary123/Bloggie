
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext _db;

        public BlogPostLikeRepository(BloggieDbContext db)
        {
            _db = db;
        }

        public async Task<BlogPostLike> AddLikesForBlogPostAsync(BlogPostLike blogPostLike)
        {
                 await _db.BlogPostLikes.AddAsync(blogPostLike);
                await _db.SaveChangesAsync();
                return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlogAsync(Guid blogPostId)
        {
            // List of likes for a particular blog post
            return await _db.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {

            // counting the likes based on blog post id
            return await _db.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
