using BackEndFinal.Data;
using BackEndFinal.Extensions;
using BackEndFinal.Models;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SpeakerController : Controller
    {
        private readonly ISpeakerService speakerService;
        private readonly IEventService eventService;

        public SpeakerController(ISpeakerService speakerService, IEventService eventService)
        {
            this.speakerService = speakerService;
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var speakers = await speakerService.GetAlSpeakerAsync(0, 0, s => s.Event);
            return View(speakers);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedSpeaker = await speakerService.GetSpeakerByIdAsync(id, s => s.Event);
            if (existedSpeaker == null) return NotFound();
            return View(existedSpeaker);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Events = new SelectList(await eventService.GetAllEventAsync(0, 0, s => s.Speakers), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerCreateVM speakerCreateVM)
        {
            ViewBag.Events = new SelectList(await eventService.GetAllEventAsync(0, 0), "Id", "Title");
            if (!ModelState.IsValid)
            {
                return View(speakerCreateVM);
            }
            Speaker speaker = new Speaker();
            var newProfileImage = speakerCreateVM.Photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("photo", "Only image files are allowed.");
                    return RedirectToAction(nameof(Index));
                }
                speaker.ImageUrl = await newProfileImage.SaveFile("teacher");

            }

            speaker.Name = speakerCreateVM.Name;
            speaker.EventId = speakerCreateVM.EventId;
            speaker.position = speakerCreateVM.Position;
            await speakerService.AddSpeakerAsync(speaker);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedSpeaker = await speakerService.GetSpeakerByIdAsync(id, s => s.Event);
            if (existedSpeaker == null) return NotFound();
            existedSpeaker?.ImageUrl?.DeleteFile();
            await speakerService.DeleteSpeakerAsync(existedSpeaker);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Events = new SelectList(await eventService.GetAllEventAsync(0, 0, s => s.Speakers), "Id", "Title");

            if (id == null) return BadRequest();
            var existedSpeaker = await speakerService.GetSpeakerByIdAsync(id, s => s.Event);
            if (existedSpeaker == null) return NotFound();
            return View(new SpeakerUpdateVM
            {
                Name = existedSpeaker.Name,
                EventId = existedSpeaker.EventId,
                Position = existedSpeaker.position,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SpeakerUpdateVM model)
        {
            ViewBag.Events = new SelectList(await eventService.GetAllEventAsync(0, 0, s => s.Speakers), "Id", "Title");

            if (id == null) return BadRequest();
            var existedSpeaker = await speakerService.GetSpeakerByIdAsync(id, s => s.Event);
            if (existedSpeaker == null) return NotFound();
            if(!ModelState.IsValid) return  View(model);
            var newProfileImage = model.Photo;
            if (newProfileImage != null)
            {
                if (!newProfileImage.CheckContentType())
                {
                    ModelState.AddModelError("photo", "Only image files are allowed.");
                    return RedirectToAction(nameof(Index));
                }

                if (!string.IsNullOrEmpty(existedSpeaker.ImageUrl))
                {
                    existedSpeaker.ImageUrl.DeleteFile("teacher");
                }

                existedSpeaker.ImageUrl = await newProfileImage.SaveFile("teacher");
               
            }

            existedSpeaker.position = model.Position;
            existedSpeaker.Name = model.Name;
            existedSpeaker.EventId = model.EventId;
            await speakerService.UpdateSpeakerAsync(existedSpeaker);
            return RedirectToAction("Index");
        }
    }
}
