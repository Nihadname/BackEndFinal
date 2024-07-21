using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using BackEndFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ICategoryService categoryService;

        public EventController(IEventService eventService, ICategoryService categoryService)
        {
            _eventService = eventService;
            this.categoryService = categoryService;
        }

        public async  Task<IActionResult> Index()
        {
         
            var events =await  _eventService.GetAllEventAsync(0, 0,  s => s.Images);
            ViewBag.EventCount = events.Count();
            return View(events);
        }
        public async Task<IActionResult> Loadmore(int skip=3)
        {
            var datas = await _eventService.GetAllEventAsync(skip, 3,  s => s.Images);
            return PartialView("_EventPartialView", datas);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if(id == null) return BadRequest();
            var existedEvent= await _eventService.GetEventByIdAsync(id,s=>s.Images,s=>s.Speakers);
            EventDetailVM eventDetail = new EventDetailVM();
            eventDetail.ProjectEvent = existedEvent;
            return View(eventDetail);
        }
        public async Task<IActionResult> EventsInCategory(int? id)
        {
            if (id == null) return BadRequest();
            var categories = categoryService.GetAllCategoryQuery();
            var categorieWithEvents = await categories.AsNoTracking().Include(s => s.events).ThenInclude(s => s.Images)
                .FirstOrDefaultAsync(s=>s.Id==id);
            if (categorieWithEvents == null) return NotFound();
            return View(categorieWithEvents);

        }
    }
}
