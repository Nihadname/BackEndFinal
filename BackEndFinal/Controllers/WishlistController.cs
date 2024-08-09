using BackEndFinal.Data;
using BackEndFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public WishlistController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized(); 

            string existedUserId = user.Id;

            var wishlistItems = await _appDbContext.wishlist
                .Where(s => s.UserId == existedUserId)
                .Include(s => s.Course)
                .ToListAsync();

            return View(wishlistItems);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToWishlist(int? CourseId)
        {
            if (CourseId == null) return BadRequest();
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            string existedUserId = user.Id;

            bool isAlreadyInWishlist = await _appDbContext.wishlist
                .AnyAsync(w => w.UserId == existedUserId && w.CourseID == CourseId.Value);

            if (isAlreadyInWishlist)
            {
                return Json(new { success = false, message = "Item is already in the wishlist" });
            }

            WishlistItem wishlistItem = new WishlistItem
            {
                UserId = existedUserId,
                CourseID = CourseId.Value
            };

            await _appDbContext.wishlist.AddAsync(wishlistItem);
            await _appDbContext.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromWishlist(int? CourseId)
        {
            if (CourseId == null) return BadRequest();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            string existedUserId = user.Id;

            var wishlistItem = await _appDbContext.wishlist
                .FirstOrDefaultAsync(w => w.UserId == existedUserId && w.CourseID == CourseId.Value);

            if (wishlistItem == null)
            {
                return Json(new { success = false, message = "Item not found in wishlist" });
            }

            _appDbContext.wishlist.Remove(wishlistItem);
            await _appDbContext.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
