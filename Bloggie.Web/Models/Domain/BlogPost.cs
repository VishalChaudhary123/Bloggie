using System.Collections.ObjectModel;

namespace Bloggie.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Establishing relation
        // One blog post can have many tags so it is one to many relationship

        public ICollection<Tag> Tags { get; set; }

        // Creating the similar properties in Tag.cs 

        // Creating relationship BlogPostLikes domain model
        public ICollection<BlogPostLike> Likes { get; set; }


        // Creating relationship BlogPostComment domain model
        public ICollection<BlogPostComment> Comments { get; set; }
    }
}
