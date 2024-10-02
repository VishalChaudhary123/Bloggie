using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
       

        public BlogPostLikeController( IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
           
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            // Map AddLikerequest to BlogPostLike domain model

            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };
             await _blogPostLikeRepository.AddLikesForBlogPostAsync(model);
            return Ok();
        }

        [HttpGet]
        [Route("{BlogPostId:Guid}/totallikes")]
        public async Task<IActionResult> GetTotalLikesForBlogPost([FromRoute] Guid BlogPostId)
        {
              var totalLikes =  await _blogPostLikeRepository.GetTotalLikes(BlogPostId);
            return Ok(totalLikes);  
        }
    }
}
