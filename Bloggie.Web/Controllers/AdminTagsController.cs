using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
                 
            };

               await _tagRepository.AddTagAsync(tag);
          
          return RedirectToAction("ListTags","AdminTags");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ListTags()
        {

            // use DbContext to read the tags
            var tags = await _tagRepository.GetAllTagsAsync();
            
            return View(tags);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult>  Edit(Guid Id)
        {

            var tag = await _tagRepository.GetTagByIdAsync(Id);

            if(tag != null)
            {

                var editTagRequest = new EditTagsRequest
                {
                    DisplayName = tag.DisplayName,
                    Id = tag.Id,
                    Name = tag.Name,

                };
                return View(editTagRequest);
            }
            return View(null);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagsRequest editTagsRequest)
        {
            // Mapping EditTagsRequest to Tag domain model
            var tag = new Tag
            {
                Name = editTagsRequest.Name,
                DisplayName = editTagsRequest.DisplayName,
                Id = editTagsRequest.Id,
            };

          var updtedTag =  await _tagRepository.UpdateTagAsync(tag);

            if (updtedTag != null) 
            {
            
            }
            else
            {

            }
         
            return RedirectToAction("Edit", new {Id = editTagsRequest.Id});
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]

        public async Task<IActionResult> Delete(EditTagsRequest editTagsRequest) 
        {
         
           var deletedTag =  await _tagRepository.DeleteTagAsync(editTagsRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("ListTags");
            }
            else
            {

            }
            return RedirectToAction("Edit", new {Id = editTagsRequest.Id});

        }


    }
}
