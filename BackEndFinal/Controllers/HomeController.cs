using BackEndFinal.Data;
using BackEndFinal.Helpers;
using BackEndFinal.Models;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackEndFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderContentService _sliderContentService;
        private readonly IOfferedAdvantageService _overlayService;
        private readonly IEventService _eventService;
        private readonly IBlogService _blogService;
        private readonly ISubscriberService _subscriberService;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ISliderService sliderService, ISliderContentService sliderContentService, IOfferedAdvantageService overlayService, IEventService eventService, AppDbContext appDbContext, IBlogService blogService, ISubscriberService subscriberService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _sliderService = sliderService;
            _sliderContentService = sliderContentService;
            _overlayService = overlayService;
            _eventService = eventService;
            _appDbContext = appDbContext;
            _blogService = blogService;
            _subscriberService = subscriberService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int page=1)
        {
           HomeViewModel model = new HomeViewModel();
            model.sliders =await _sliderService
                .GetAllSlidersAsync(0, 0, s=>s.SliderContent);
            model.offeredAdvantages = await _overlayService
                .GetAllOfferedAdvantagesAsync(0, 3);
            ICollection<Event> events = await _eventService
                .GetAllEventAsync(0, 8, s => s.Speakers, skip => skip.Category);
            model.events = events
                .OrderByDescending(e => e.HeldTime)
                .ToList();
           model.WhyChoose= await _appDbContext.whyChooses
                .AsNoTracking()
                .FirstOrDefaultAsync();
            //model.testimonialAreas=await _appDbContext.testimonialAreas.AsNoTracking().ToListAsync();
            var query = _blogService.GetAllBlogQuery();
            var blogsQuery = query.Include(s => s.Images).AsNoTracking();
            var paginatedBlogs = PaginationVM<Blog>.Create(blogsQuery, page, 3);
            model.PaginatedBlogs = paginatedBlogs;

            return View(model);
        }
        [HttpPost]
       public async Task<IActionResult> SendEmailToSubscribe(SubscriberVM subscriber)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErroreEmailMessage"] = "dont write empty value";
                return RedirectToAction(nameof(Index));
            }
            if (await _subscriberService.GetAllSubscriberQuery().AnyAsync(s => s.EmailAddress.ToLower() == subscriber.EmailAddress))
            {

                ModelState.AddModelError("EmailAddress", "email already exists");

                TempData["TheSameEmailMessage"] = "email already exists";
                return RedirectToAction(nameof(Index));
            }
            var newSubscriber = new Subscriber() { 
                EmailAddress = subscriber.EmailAddress,
            };
await _subscriberService.AddSubscriberAsync(newSubscriber);
            TempData["SuccessMessage"] = "Subscription successful!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedData()
        {
            foreach (var item in Enum.GetValues(typeof(RolesEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }

            var adminUser = new AppUser() { Email = "info@meeting.az", FullName = "Super Admin" };
            var Student = new AppUser() { Email = "Student@meeting.az", FullName = "Student2", UserName = "Student1" };
            string password = "Raska2024!!";
            string password2 = "Salam12@";
            IdentityResult result = await _userManager.CreateAsync(adminUser, password);
            IdentityResult result1 = await _userManager.CreateAsync(Student, password2);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Member");

            }
            if (result1.Succeeded)
            {
                await _userManager.AddToRoleAsync(Student, "Admin");

            }
            return Content("OK");
        }
    }
}
