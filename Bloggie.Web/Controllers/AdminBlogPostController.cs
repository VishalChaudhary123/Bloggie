using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get tags from repository
            var tags = await _tagRepository.GetAllTagsAsync();

          var model = new AddBlogPostRequest
          {
              Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
          };

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add (AddBlogPostRequest addBlogPostRequest)
        {
            //Map viewmodel to domain model
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                Author = addBlogPostRequest.Author,
                UrlHandle = addBlogPostRequest.UrlHandle,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                ShortDescription = addBlogPostRequest.ShortDescription,
                PublishedDate = addBlogPostRequest.PublishedDate,
                 Visible = addBlogPostRequest.Visible,
                 
            };

            // map tags to selected tags
            var selectedTags = new List<Tag>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTag)
            {
                var selectedTagidasGuid = Guid.Parse(selectedTagId);
               var existingtag =  await _tagRepository.GetTagByIdAsync(selectedTagidasGuid);
                if (existingtag != null) 
                {
                    selectedTags.Add(existingtag);
                }
            } 

            // Mapping tags back to domain model

            blogpost.Tags = selectedTags;

                await _blogPostRepository.AddBlogAsync(blogpost);

              
            return RedirectToAction("Add");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
             var allBlogs =   await _blogPostRepository.GetAllBlogAsync();
            return View(allBlogs);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingBlog = await _blogPostRepository.GetBlogByIdAsync(id);
            var allTags = await _tagRepository.GetAllTagsAsync();
            // Map Domain model to viewmodel

            if (existingBlog != null) 
            {
                var model = new EditBlogPostRequest
                {
                    Id = existingBlog.Id,
                    Heading = existingBlog.Heading,
                    PublishedDate = existingBlog.PublishedDate,
                    Content = existingBlog.Content,
                    PageTitle = existingBlog.PageTitle,
                    Author = existingBlog.Author,
                    FeaturedImageUrl = existingBlog.FeaturedImageUrl,
                    ShortDescription = existingBlog.ShortDescription,
                    UrlHandle = existingBlog.UrlHandle,
                    Visible = existingBlog.Visible,
                    Tags = allTags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    SelectedTag = existingBlog.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }
          
            return View(null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Edit")]
        public  async Task<IActionResult> Edit (EditBlogPostRequest editBlogPostRequest)
        {
            // map view model back to domain model

            var blogPostdomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                Content = editBlogPostRequest.Content,
                PageTitle = editBlogPostRequest.PageTitle,
                Author = editBlogPostRequest.Author,
                PublishedDate = editBlogPostRequest.PublishedDate,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Visible = editBlogPostRequest.Visible,

            };
            // Map tags into domain model

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTag)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                  var foundTag =  await _tagRepository.GetTagByIdAsync(tag);
                    if (foundTag != null) 
                    {
                      selectedTags.Add(foundTag);
                    }
                }
            }
            // Adding tag to blogpost domain model

            blogPostdomainModel.Tags = selectedTags;

            // Update the details 

             var updatedblog=  await _blogPostRepository.UpdateBlogAsync(blogPostdomainModel);

            if (updatedblog != null) 
            {
              return  RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog (EditBlogPostRequest blog)
        {
            var deletedPost = await _blogPostRepository.DeleteBlogAsync(blog.Id);
            if (deletedPost != null) 
            {
              
                return RedirectToAction("BlogList");
            }
            return RedirectToAction("Edit", new { id = blog.Id });
        }
    }
}
