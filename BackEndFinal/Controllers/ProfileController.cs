using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return NotFound();
            var userProfileVM = new UserProfileVM
            {
                FullName = currentUser.FullName,
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                imageUrl = currentUser.imageUrl
            };

            return View(userProfileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(UserProfileVM userProfileVM)
        {
            var existingUser = await _userManager.GetUserAsync(User);
            if (existingUser == null) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            var newProfileImage = userProfileVM.photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed.");
                    return RedirectToAction(nameof(Index));
                }
                if (!newProfileImage.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "The image size is too large. Maximum allowed size is 500KB.");
                    return RedirectToAction(nameof(Index));
                }

                // Delete the old image file if it exists
                if (!string.IsNullOrEmpty(existingUser.imageUrl))
                {
                    existingUser.imageUrl.DeleteFile();
                }

                // Save the new image file
                existingUser.imageUrl = await newProfileImage.SaveFile();
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
