using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepostitory;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostComment)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _blogPostCommentRepostitory = blogPostComment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var existingBlog = await _blogPostRepository.GetBlogByUrlHandleAsync(urlHandle);

            var blogPostLikesViewModel = new BlogDetailsViewModel();
            if (existingBlog != null)
            {
                var totalBlogPostLikes = await _blogPostLikeRepository.GetTotalLikes(existingBlog.Id);

                if (_signInManager.IsSignedIn(User))
                {
                    // Get like for this user for this blog

                    var likesForBlog = await _blogPostLikeRepository.GetLikesForBlogAsync(existingBlog.Id);

                    // checking if singed in user has liked this post

                    var userId = _userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likeFromuser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

                        liked = likeFromuser != null; // will set True if likeFromuser is not null

                    }
                }

                // Get Comment for blog post

                var blogCommentsDomainModel = await _blogPostCommentRepostitory.GetCommentByBlogId(existingBlog.Id);

                var blogCommentsforView = new List<BlogComment>();
                foreach (var blogComment in blogCommentsDomainModel)
                {
                    blogCommentsforView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
                }

                // Map domain Model to BlogDetailsViewModel

                blogPostLikesViewModel = new BlogDetailsViewModel
                {
                    Id = existingBlog.Id,
                    Author = existingBlog.Author,
                    Content = existingBlog.Content,
                    FeaturedImageUrl = existingBlog.FeaturedImageUrl,
                    Heading = existingBlog.Heading,
                    PageTitle = existingBlog.PageTitle,
                    PublishedDate = existingBlog.PublishedDate,
                    ShortDescription = existingBlog.ShortDescription,
                    Tags = existingBlog.Tags,
                    UrlHandle = urlHandle,
                    Visible = existingBlog.Visible,
                    TotalLikes = totalBlogPostLikes,
                    Liked = liked,
                    Comments = blogCommentsforView,


                };
            }
            return View(blogPostLikesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index (BlogDetailsViewModel blogDetailsViewModel)
        {
            // Map viewmodel to domain model

            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now,
                };

                await _blogPostCommentRepostitory.AddCommentAsync(domainModel);
                return RedirectToAction("Index", "Blogs", new {urlHandle = blogDetailsViewModel.UrlHandle});
            }
            return View();
            
        }
    }
}
