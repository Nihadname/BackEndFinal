using BackEndFinal.Models;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(s => s.UserName.ToLower().Contains(searchText.ToLower()) ||
                                         s.FullName.ToLower().Contains(searchText.ToLower()));
            }

            var usersForActualForm = await users.ToListAsync();
            return View(usersForActualForm);
        }
        public async Task<IActionResult> Detail(string? id)
        {
            if (id == null) return BadRequest();
            var existedUser= await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(existedUser);
            if (existedUser == null) return NotFound();
            ViewBag.UserRoles = userRoles;  
            return View(existedUser);
        }
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (id == null) return BadRequest();
            var currentUser=await _userManager.FindByIdAsync(id);
            if (currentUser == null) return NotFound();
            currentUser.IsBlocked=!currentUser.IsBlocked;
            await _userManager.UpdateAsync(currentUser);
            return RedirectToAction("Index");   
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            var existedUser = await _userManager.FindByIdAsync(id);
            if (existedUser == null) return NotFound();
            IdentityResult result = await _userManager.DeleteAsync(existedUser);
            if (result.Succeeded)
            {
                TempData["SuccessdeleteUser"] = "User deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UpdateUserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber=user.PhoneNumber
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,UpdateUserViewModel userViewModel)
        {
            if (id == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            if (!ModelState.IsValid) return View(userViewModel);
            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;
            user.FullName = userViewModel.FullName;
            user.PhoneNumber = userViewModel.PhoneNumber;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessUpdateUser"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Index");

        }


    }
}
