using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")] 
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        private readonly AppDbContext appDbContext;

        public EventController(IEventService eventService, ICategoryService categoryService, AppDbContext appDbContext)
        {
            this.eventService = eventService;
            this.categoryService = categoryService;
            this.appDbContext = appDbContext;
        }

        public IActionResult Index(string searchText)
        {
            var events = eventService.GetAllEventQuery();
            if (!string.IsNullOrEmpty(searchText))
            {

                events = events.Where(s => s.Title.ToLower().Contains(searchText.ToLower()) ||
                                         s.Description.ToLower().Contains(searchText.ToLower()));
            }
            var usersForActualForm = events.Include(s => s.Images).Include(s => s.Category).ToList();
            return View(usersForActualForm);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM eventCreateVM)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            if (!ModelState.IsValid) return View(eventCreateVM);

            // Validate StartTime format
            if (!TimeSpan.TryParseExact(eventCreateVM.StartTime.ToString(@"hh\:mm"), @"hh\:mm", CultureInfo.InvariantCulture, out var startTime))
            {
                ModelState.AddModelError("StartTime", "Invalid StartTime format. Use hh:mm.");
                return View(eventCreateVM);
            }

            // Validate EndTime format
            if (!TimeSpan.TryParseExact(eventCreateVM.EndTime.ToString(@"hh\:mm"), @"hh\:mm", CultureInfo.InvariantCulture, out var endTime))
            {
                ModelState.AddModelError("EndTime", "Invalid EndTime format. Use hh:mm.");
                return View(eventCreateVM);
            }

            var files = eventCreateVM.Photos;
            if (files == null || files.Length == 0)
            {
                ModelState.AddModelError("Photos", "Images cannot be null");
                return View(eventCreateVM);
            }

            Event newEvent = new Event
            {
                Title = eventCreateVM.Title,
                Description = eventCreateVM.Description,
                Location = eventCreateVM.Location,
                CategoryId = eventCreateVM.CategoryId,
                StartTime = startTime,
                EndTime = endTime,
                HeldTime = eventCreateVM.HeldTime
            };

            List<EventImage> images = new List<EventImage>();
            foreach (var newProfileImage in files)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photos", "Only image files are allowed.");
                    return View(eventCreateVM);
                }
                if (!newProfileImage.CheckSize(500))
                {
                    ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                    return View(eventCreateVM);
                }

                EventImage newImage = new EventImage
                {
                    Name = await newProfileImage.SaveFile(),
                    EventId = newEvent.Id,
                    IsMain = files[0] == newProfileImage
                };
                images.Add(newImage);
            }

            newEvent.Images = images; 

            await eventService.AddEventAsync(newEvent);
            return RedirectToAction("Index");
        }


    }
}
