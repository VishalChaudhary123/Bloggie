namespace Bloggie.Web.Models.Domain
{
    public class BlogPostLike
    {

        public Guid Id { get; set; }

        // Blog Post Id that is Likes

        public Guid BlogPostId { get; set; }

        // User Id who liked it - 

        public Guid UserId { get; set; }

        // Add the navigation property of likes to BlogPost domain model
    }
}
