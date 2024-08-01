using BackEndFinal.Data;
using BackEndFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class CourseRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public CourseRequestController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RequestCourse(string courseName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var courseRequest = new CourseRequest
            {
                CourseName = courseName,
                UserName = user.UserName,
                IsApproved = false
            };

            _context.CourseRequests.Add(courseRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }
    }
}
