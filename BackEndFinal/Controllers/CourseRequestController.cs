using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndFinal.Controllers
{
    public class CourseRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public CourseRequestController(UserManager<AppUser> userManager, AppDbContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _context = context;
            _emailService = emailService;
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
            _emailService.SendEmail(
               from: "nihadmi@code.edu.az",
               to: user.Email,
               subject: "kurs almaq ile Bagli",
               body: "Salam, sizin kurs alma isteyniz qeyde alinmisidir  bir muddet sonra size gorus vaxti teyin olunacaq ",
               smtpHost: "smtp.gmail.com",
               smtpPort: 587,
               enableSsl: true,
               smtpUser: "nihadmi@code.edu.az",
               smtpPass: "ilyo ibry uphi gnfe\r\n"
               );
            return RedirectToAction("Index", "Profile");
        }
    }
}
