﻿using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CloudinaryDotNet.Actions;


namespace Bloggie.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult>List()
        {

            var users = await _userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email,
                });
            }
            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,

            };
           var identityResult =   await _userManager.CreateAsync(identityUser,request.Password);
            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // assign roles to this user

                    var roles = new List<string> { "User" };
                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }
                    
                    foreach (var role in roles)
                    {
                        identityResult = await _userManager.AddToRoleAsync(identityUser, role);
                    }
                    if (identityResult is not null && identityResult.Succeeded) 
                    {
                        return RedirectToAction("List","AdminUsers");
                    }
                        
                }
            }
            return View();
        }

        [HttpPost]
        //[Route("{Guid:id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user =  await _userManager.FindByIdAsync(id.ToString());
            if (user is not null)
            {
                var identityResult = await _userManager.DeleteAsync(user);
                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List","AdminUsers");
                }
            }
            return View();
        }
    }
}
