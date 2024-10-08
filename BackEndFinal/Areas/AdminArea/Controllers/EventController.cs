﻿using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
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
            var usersForActualForm = events.Include(s => s.Images).Include(s => s.Category).Include(s=>s.Speakers).ToList();
            return View(usersForActualForm);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            //ViewBag.Speakers = new SelectList(await appDbContext.speakers.AsNoTracking().ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM eventCreateVM)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            //ViewBag.Speakers = new SelectList(await appDbContext.speakers.AsNoTracking().ToListAsync(), "Id", "Name");

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
                StartTime = eventCreateVM.StartTime,
                EndTime = eventCreateVM.EndTime,
                HeldTime = eventCreateVM.HeldTime,
                //Speakers = new List<Speaker>()

            };
            //foreach (var speakerId in eventCreateVM.SelectedSpeakerIds)
            //{
            //    var speaker = await appDbContext.speakers.FindAsync(speakerId);
            //    if (speaker != null)
            //    {
            //        newEvent.Speakers.Add(speaker);
            //    }
            //}
            List<EventImage> images = new List<EventImage>();
            foreach (var newProfileImage in files)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("Photos", "Only image files are allowed.");
                    return View(eventCreateVM);
                }
                //if (!newProfileImage.CheckSize(500))
                //{
                //    ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                //    return View(eventCreateVM);
                //}

                EventImage newImage = new EventImage
                {
                    Name = await newProfileImage.SaveFile("event"),
                    EventId = newEvent.Id,
                    IsMain = files[0] == newProfileImage
                };
                images.Add(newImage);
            }

            newEvent.Images = images; 

            await eventService.AddEventAsync(newEvent);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedBlog = await eventService.GetEventByIdAsync(id, s => s.Images, s => s.Category);
            if (existedBlog == null) return NotFound();
            return View(existedBlog);
        }

         public async Task<IActionResult> SetMainPhoto(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.eventImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();

            var mainImage = await appDbContext.eventImages
                .FirstOrDefaultAsync(y => y.IsMain == true && y.EventId == existedPhoto.EventId);
            if (mainImage != null) mainImage.IsMain = false;

            existedPhoto.IsMain = true;
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = existedPhoto.EventId });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedEvent = await appDbContext.events.FirstOrDefaultAsync(x => x.Id == id);
            if (existedEvent == null) return NotFound();
            foreach (var image in existedEvent.Images)
            {
                image.Name.DeleteFile("event");

            }
            appDbContext.events.Remove(existedEvent);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
            //ViewBag.Speakers = new SelectList(await appDbContext.speakers.AsNoTracking().ToListAsync(), "Id", "Name");
            var existedEvent = await appDbContext.events.Include(s=>s.Images).Include(s=>s.Category).Include(s=>s.Speakers).FirstOrDefaultAsync(x => x.Id == id);
            if (existedEvent == null) return NotFound();
            return View(new EventUpdateVM
            {
                Title = existedEvent.Title,
                Description = existedEvent.Description,
                Location = existedEvent.Location,
                CategoryId = existedEvent.CategoryId,
                StartTime = existedEvent.StartTime,
                EndTime = existedEvent.EndTime,
                HeldTime = existedEvent.HeldTime,
                Images= existedEvent.Images,
                //SelectedSpeakerIds = existedEvent.Speakers.Select(s => s.Id).ToList()

            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, EventUpdateVM eventUpdateVM)
        {
            if (id == null) return BadRequest();
            var existedEvent = await appDbContext.events
                .Include(s => s.Images)
                .Include(s => s.Category)
                .Include(s => s.Speakers)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existedEvent == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
                //ViewBag.Speakers = new SelectList(await appDbContext.speakers.AsNoTracking().ToListAsync(), "Id", "Name");
                eventUpdateVM.Images = existedEvent.Images;
                return View(eventUpdateVM);
            }

            var files = eventUpdateVM.Photos;
            if (files != null && files.Length > 0)
            {
                if (files.Length > 4)
                {
                    ViewBag.Categories = new SelectList(await categoryService.GetAllCategoryAsync(0, 0), "Id", "Name");
                    //ViewBag.Speakers = new SelectList(await appDbContext.speakers.AsNoTracking().ToListAsync(), "Id", "Name");
                    eventUpdateVM.Images = existedEvent.Images;

                    ModelState.AddModelError("Photos", "Minimum 4 Photos!");
                    return View(eventUpdateVM);
                }

                List<EventImage> newImages = new List<EventImage>();
                foreach (var file in files)
                {
                    if (!file.CheckContentType())
                    {
                        ModelState.AddModelError("Photos", "Choose the right type!");
                        return View(eventUpdateVM);
                    }

                    var newImage = new EventImage
                    {
                        Name = await file.SaveFile("event"),
                        EventId = existedEvent.Id,
                        IsMain = files[0] == file
                    };

                    newImages.Add(newImage);
                }

                existedEvent.Images = newImages;
            }

            existedEvent.Title = eventUpdateVM.Title;
            existedEvent.Description = eventUpdateVM.Description;
            existedEvent.Location = eventUpdateVM.Location;
            existedEvent.CategoryId = eventUpdateVM.CategoryId;
            existedEvent.StartTime = eventUpdateVM.StartTime;
            existedEvent.EndTime = eventUpdateVM.EndTime;
            existedEvent.HeldTime = eventUpdateVM.HeldTime;

            appDbContext.events.Update(existedEvent);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.eventImages.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            try
            {
                existedPhoto.Name.DeleteFile("event");
                appDbContext.eventImages.Remove(existedPhoto);
                await appDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
            return RedirectToAction("Update", new { id = existedPhoto.EventId });

        }

    }
}
