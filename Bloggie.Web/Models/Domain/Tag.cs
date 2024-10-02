namespace Bloggie.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        // One tag can be related to many blog posts
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
