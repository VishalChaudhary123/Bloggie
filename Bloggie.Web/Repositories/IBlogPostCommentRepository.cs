using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentByBlogId (Guid blogPostId);
    }
}
