using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogAsync();
        Task<BlogPost?> GetBlogByIdAsync(Guid id);

        Task<BlogPost> AddBlogAsync(BlogPost blog);

        Task<BlogPost?> UpdateBlogAsync(BlogPost blog);

        Task<BlogPost?> DeleteBlogAsync(Guid id);

        Task<BlogPost?> GetBlogByUrlHandleAsync(string urlHandle);
    }
}
