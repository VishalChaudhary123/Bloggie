using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes (Guid blogPostId);

        Task<BlogPostLike> AddLikesForBlogPostAsync (BlogPostLike blogPostLike);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlogAsync (Guid blogPostId);
    }
}
