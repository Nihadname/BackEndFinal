using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext appDbContext;
        public ProfileController(UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return NotFound();
            var courseRequests = await appDbContext.CourseRequests
               .Where(cr => cr.UserName == currentUser.UserName)
               .ToListAsync();
            var userProfileVM = new UserProfileVM
            {
                FullName = currentUser.FullName,
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                imageUrl = currentUser.imageUrl,
                                CourseRequests = courseRequests 

            };

            return View(userProfileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(PhotoVM userProfileVM)
        {
            var existingUser = await _userManager.GetUserAsync(User);
            if (existingUser == null) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            var newProfileImage = userProfileVM.photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("photo", "Only image files are allowed.");
                    return RedirectToAction(nameof(Index));
                }

                if (!string.IsNullOrEmpty(existingUser.imageUrl))
                {
                    existingUser.imageUrl.DeleteFile("teacher");
                }

                // Save the new image file
                existingUser.imageUrl = await newProfileImage.SaveFile("teacher");
                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to update user profile.");
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
